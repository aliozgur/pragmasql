using System;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;

namespace PragmaSQL.Core
{
    /// <summary>
    /// Safe source/factory for SqlConnection that preserves the raw connection string
    /// (never log it), exposes a redacted form for diagnostics, and creates connections on demand.
    /// </summary>
    public sealed class PragmaSqlConnectionSource
    {
        private readonly string _raw;
        private readonly SqlConnectionStringBuilder _builder;

        public PragmaSqlConnectionSource(string connectionString, Action<SqlConnectionStringBuilder> tweak = null)
        {
            if (string.IsNullOrWhiteSpace(connectionString))
                throw new ArgumentException("Connection string is required.", nameof(connectionString));

            _raw = connectionString.Trim();
            _builder = new SqlConnectionStringBuilder(_raw);

            // Optional: let caller enforce org-wide conventions (e.g., Application Name)
            tweak?.Invoke(_builder);

            // Keep _raw exactly as provided (don’t rewrite). We only rewrite for redaction/logs.
        }

        /// <summary>Raw connection string as provided (contains secrets). Do NOT log.</summary>
        public string Raw => _raw;

        /// <summary>Safe for logs (password removed; preserves other keys).</summary>
        public string Redacted => Redact(_raw);

        /// <summary>Convenience accessors.</summary>
        public string DataSource => _builder.DataSource ?? string.Empty;
        public string InitialCatalog => _builder.InitialCatalog ?? string.Empty;
        public string UserId => _builder.UserID ?? string.Empty;
        public bool HasPassword => !string.IsNullOrEmpty(_builder.Password);

        /// <summary>Create a closed SqlConnection (caller must dispose and open).</summary>
        public SqlConnection Create() => new SqlConnection(_raw);

        /// <summary>Create and open a SqlConnection (sync). Caller must dispose.</summary>
        public SqlConnection CreateOpen()
        {
            var conn = new SqlConnection(_raw);
            conn.Open();
            return conn;
        }

        /// <summary>Create and open a SqlConnection (async). Caller must dispose.</summary>
        public async Task<SqlConnection> CreateOpenAsync(CancellationToken ct = default)
        {
            var conn = new SqlConnection(_raw);
            await conn.OpenAsync(ct).ConfigureAwait(false);
            return conn;
        }

        /// <summary>
        /// Scoped usage helper (sync): opens a connection, runs the action, disposes the connection.
        /// </summary>
        public T WithConnection<T>(Func<SqlConnection, T> work)
        {
            if (work is null) throw new ArgumentNullException(nameof(work));
            using (var conn = CreateOpen())
            {
                return work(conn);
            }
        }

        /// <summary>
        /// Scoped usage helper (async): opens a connection, runs the func, disposes the connection.
        /// </summary>
        public async Task<T> WithConnectionAsync<T>(Func<SqlConnection, Task<T>> work, CancellationToken ct = default)
        {
            if (work is null) throw new ArgumentNullException(nameof(work));
            using (var conn = await CreateOpenAsync(ct).ConfigureAwait(false))
            {
                return await work(conn).ConfigureAwait(false);
            }
        }

        public override string ToString() => Redacted;

        /// <summary>Create a source from an env var (throws if missing).</summary>
        public static PragmaSqlConnectionSource FromEnvironment(string envVarName, Action<SqlConnectionStringBuilder> tweak = null)
        {
            var value = Environment.GetEnvironmentVariable(envVarName);
            if (string.IsNullOrWhiteSpace(value))
                throw new InvalidOperationException($"Environment variable '{envVarName}' is not set.");
            return new PragmaSqlConnectionSource(value, tweak);
        }

        private static string Redact(string cs)
        {
            var b = new SqlConnectionStringBuilder(cs);
            // Strip password under both canonical and synonym keys
            b.Password = string.Empty;
            if (b.ContainsKey("Pwd")) b["Pwd"] = string.Empty;
            return b.ToString();
        }
    }
}
