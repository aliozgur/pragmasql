using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Xml.Serialization;
using System.Xml;
using System.IO;
using System.Reflection;
using System.Threading;

using AsynchronousCodeBlocks;

namespace WinTestAsyncCodeBlock
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void AsyncSample()
        {
            SqlConnection con = new SqlConnection("server=.;database=AsyncTestDB;uid=sa;pwd=AdiTanuL1$");
            con.Open();
            new async(delegate
            {
                int age = 28;
                string cmdStr = string.Format("insert into Address values ('{0}',{1},'{2}')",
                    "Name_" + age.ToString(), age, "Address_" + age.ToString());
                SqlCommand cmd = new SqlCommand(cmdStr, con);
                cmd.ExecuteNonQuery();
            });
        }

        private void WaitableAsyncSample()
        {
            SqlConnection con = new SqlConnection("server=.;database=AsyncTestDB;uid=sa;pwd=AdiTanuL1$");
            con.Open();
            waitableasync obj = new waitableasync(delegate
            {
                int age = 28;
                string cmdStr = string.Format("insert into Address values ('{0}',{1},'{2}')",
                    "Name_" + age.ToString(), age, "Address_" + age.ToString());
                SqlCommand cmd = new SqlCommand(cmdStr, con);
                cmd.ExecuteNonQuery();
            });
            obj.Wait(-1);
            MessageBox.Show("Done");
        }

        private void UserCreatedThreadPoolSample()
        {
            int maxPrimeNum = 10000;
            int iFirstHalfStart = 1;
            int iFirstHalfEnd = maxPrimeNum / 2;
            int iSecondHalfStart = iFirstHalfEnd + 1;
            int iSecondHalfEnd = maxPrimeNum;

            int countPrime = 0;

            long st = DateTime.Now.Ticks;
            waitableasync firstHalf = new waitableasync(delegate
            {
                for (int i = iFirstHalfStart; i <= iFirstHalfEnd; i++)
                {
                    bool isPrime = true;
                    for (int j = 2; j <= (i / 2); j++)
                    {
                        if ((i % j) == 0)
                        {
                            isPrime = false;
                            break;
                        }
                    }
                    if (isPrime == true)
                        Interlocked.Increment(ref countPrime);
                }
            }, _myThreadPool);

            waitableasync secondHalf = new waitableasync(delegate
            {
                for (int i = iSecondHalfStart; i <= iSecondHalfEnd; i++)
                {
                    bool isPrime = true;
                    for (int j = 2; j <= (i / 2); j++)
                    {
                        if ((i % j) == 0)
                        {
                            isPrime = false;
                            break;
                        }
                    }
                    if (isPrime == true)
                        Interlocked.Increment(ref countPrime);
                }
            }, _myThreadPool);

            firstHalf.Wait(-1);
            secondHalf.Wait(-1);

            long et = DateTime.Now.Ticks;
            MessageBox.Show(string.Format("Prime count: {0}, Total time: {1}", countPrime, ((et - st) / 10000).ToString()));
        }

        private void WaitAllExSample()
        {
            int maxPrimeNum = 10000;
            int iFirstHalfStart = 1;
            int iFirstHalfEnd = maxPrimeNum / 2;
            int iSecondHalfStart = iFirstHalfEnd + 1;
            int iSecondHalfEnd = maxPrimeNum;

            int countPrime = 0;

            long st = DateTime.Now.Ticks;
            waitableasync firstHalf = new waitableasync(delegate
            {
                for (int i = iFirstHalfStart; i <= iFirstHalfEnd; i++)
                {
                    bool isPrime = true;
                    for (int j = 2; j <= (i / 2); j++)
                    {
                        if ((i % j) == 0)
                        {
                            isPrime = false;
                            break;
                        }
                    }
                    if (isPrime == true)
                        Interlocked.Increment(ref countPrime);
                }
            }, _myThreadPool);

            waitableasync secondHalf = new waitableasync(delegate
            {
                for (int i = iSecondHalfStart; i <= iSecondHalfEnd; i++)
                {
                    bool isPrime = true;
                    for (int j = 2; j <= (i / 2); j++)
                    {
                        if ((i % j) == 0)
                        {
                            isPrime = false;
                            break;
                        }
                    }
                    if (isPrime == true)
                        Interlocked.Increment(ref countPrime);
                }
            }, _myThreadPool);

            waitableasync[] arr = new waitableasync[] { firstHalf, secondHalf };
            waitableasync.WaitAllEx(arr, -1);

            long et = DateTime.Now.Ticks;
            MessageBox.Show(string.Format("Prime count: {0}, Total time: {1}", countPrime, ((et - st) / 10000).ToString()));
        }

        private void ExecutionCompletionDelegateSample()
        {
            int maxPrimeNum = 10000;
            int iFirstHalfStart = 1;
            int iFirstHalfEnd = maxPrimeNum / 2;
            int iSecondHalfStart = iFirstHalfEnd + 1;
            int iSecondHalfEnd = maxPrimeNum;

            int countPrime = 0;

            long st = DateTime.Now.Ticks;
            waitableasync firstHalf = new waitableasync(delegate
            {
                for (int i = iFirstHalfStart; i <= iFirstHalfEnd; i++)
                {
                    bool isPrime = true;
                    for (int j = 2; j <= (i / 2); j++)
                    {
                        if ((i % j) == 0)
                        {
                            isPrime = false;
                            break;
                        }
                    }
                    if (isPrime == true)
                        Interlocked.Increment(ref countPrime);
                }
            }, _myThreadPool, delegate(async objAsync)
            {
                waitableasync secondHalf = new waitableasync(delegate
                {
                    for (int i = iSecondHalfStart; i <= iSecondHalfEnd; i++)
                    {
                        bool isPrime = true;
                        for (int j = 2; j <= (i / 2); j++)
                        {
                            if ((i % j) == 0)
                            {
                                isPrime = false;
                                break;
                            }
                        }
                        if (isPrime == true)
                            Interlocked.Increment(ref countPrime);
                    }
                }, _myThreadPool, delegate(async objAsyncInner)
                {
                    long et = DateTime.Now.Ticks;
                    MessageBox.Show(string.Format("Prime count: {0}, Total time: {1}", countPrime, ((et - st) / 10000).ToString()));
                });
            });
        }

        private void DependentAsyncSample()
        {
            int maxPrimeNum = 10000;
            int iFirstHalfStart = 1;
            int iFirstHalfEnd = maxPrimeNum / 2;
            int iSecondHalfStart = iFirstHalfEnd + 1;
            int iSecondHalfEnd = maxPrimeNum;

            int countPrime = 0;

            long st = DateTime.Now.Ticks;
            waitableasync firstHalf = new waitableasync(delegate
            {
                for (int i = iFirstHalfStart; i <= iFirstHalfEnd; i++)
                {
                    bool isPrime = true;
                    for (int j = 2; j <= (i / 2); j++)
                    {
                        if ((i % j) == 0)
                        {
                            isPrime = false;
                            break;
                        }
                    }
                    if (isPrime == true)
                        Interlocked.Increment(ref countPrime);
                }
            }, _myThreadPool);

            waitableasync secondHalf = new waitableasync(delegate
            {
                for (int i = iSecondHalfStart; i <= iSecondHalfEnd; i++)
                {
                    bool isPrime = true;
                    for (int j = 2; j <= (i / 2); j++)
                    {
                        if ((i % j) == 0)
                        {
                            isPrime = false;
                            break;
                        }
                    }
                    if (isPrime == true)
                        Interlocked.Increment(ref countPrime);
                }
            }, _myThreadPool, firstHalf);

            secondHalf.Wait(-1);
            
            long et = DateTime.Now.Ticks;
            MessageBox.Show(string.Format("Prime count: {0}, Total time: {1}", countPrime, ((et - st) / 10000).ToString()));
        }

        private void DependentAsyncArraySample()
        {
            int maxPrimeNum = 10000;
            int iFirstHalfStart = 1;
            int iFirstHalfEnd = maxPrimeNum / 2;
            int iSecondHalfStart = iFirstHalfEnd + 1;
            int iSecondHalfEnd = maxPrimeNum;

            int countPrime = 0;

            long st = DateTime.Now.Ticks;
            waitableasync firstHalf = new waitableasync(delegate
            {
                for (int i = iFirstHalfStart; i <= iFirstHalfEnd; i++)
                {
                    bool isPrime = true;
                    for (int j = 2; j <= (i / 2); j++)
                    {
                        if ((i % j) == 0)
                        {
                            isPrime = false;
                            break;
                        }
                    }
                    if (isPrime == true)
                        Interlocked.Increment(ref countPrime);
                }
            }, _myThreadPool);

            waitableasync secondHalf = new waitableasync(delegate
            {
                for (int i = iSecondHalfStart; i <= iSecondHalfEnd; i++)
                {
                    bool isPrime = true;
                    for (int j = 2; j <= (i / 2); j++)
                    {
                        if ((i % j) == 0)
                        {
                            isPrime = false;
                            break;
                        }
                    }
                    if (isPrime == true)
                        Interlocked.Increment(ref countPrime);
                }
            }, _myThreadPool, new waitableasync[] { firstHalf } );

            secondHalf.Wait(-1);

            long et = DateTime.Now.Ticks;
            MessageBox.Show(string.Format("Prime count: {0}, Total time: {1}",countPrime,((et - st) / 10000).ToString()));
        }

        private void ExceptionHandlingSample()
        {
            SqlConnection con = new SqlConnection("server=.;database=AsyncTestDB;uid=sa;pwd=AdiTanuL1$");
            con.Open();
            waitableasync obj = new waitableasync(delegate
            {
                int age = 28;
                string cmdStr = string.Format("insert into Address values ('{0}',{1},'{2}')",
                    "Name_" + age.ToString(), age, "Address_" + age.ToString());
                SqlCommand cmd = new SqlCommand(cmdStr, con);
                cmd.ExecuteNonQuery();
            }, _myThreadPool, delegate(async objAsync)
            {
                if (con.State == ConnectionState.Open) con.Close();
                if (objAsync.CodeException != null)
                    MessageBox.Show(string.Format("Failed. Error: {0}",objAsync.CodeException.Message));
                else
                    MessageBox.Show("Successfully completed the insert");
            });
        }

        private void WinFormsControlUpdateSample()
        {
            SynchronizationContext sc = SynchronizationContext.Current;
            int maxPrimeNum = 10000;
            int iFirstHalfStart = 1;
            int iFirstHalfEnd = maxPrimeNum / 2;
            int iSecondHalfStart = iFirstHalfEnd + 1;
            int iSecondHalfEnd = maxPrimeNum;

            int countPrime = 0;

            long st = DateTime.Now.Ticks;
            waitableasync firstHalf = new waitableasync(delegate
            {
                for (int i = iFirstHalfStart; i <= iFirstHalfEnd; i++)
                {
                    bool isPrime = true;
                    for (int j = 2; j <= (i / 2); j++)
                    {
                        if ((i % j) == 0)
                        {
                            isPrime = false;
                            break;
                        }
                    }
                    if (isPrime == true)
                    {
                        Interlocked.Increment(ref countPrime);
                        string sMsg = string.Format("I: {0}, Thread ID: {1}", i, Thread.CurrentThread.ManagedThreadId);
                        sc.Send(delegate(object state) { listBox1.Items.Add(sMsg); }, null);
                    }
                }
            }, _myThreadPool);
            
            waitableasync secondHalf = new waitableasync(delegate 
            {
                for (int i = iSecondHalfStart; i <= iSecondHalfEnd; i++)
                {
                    bool isPrime = true;
                    for (int j = 2; j <= (i / 2); j++)
                    {
                        if ((i % j) == 0)
                        {
                            isPrime = false;
                            break;
                        }
                    }
                    if (isPrime == true)
                    {
                        Interlocked.Increment(ref countPrime);
                        string sMsg = string.Format("I: {0}, Thread ID: {1}", i, Thread.CurrentThread.ManagedThreadId);
                        sc.Send(delegate(object state) { listBox1.Items.Add(sMsg); }, null);
                    }
                }
            },_myThreadPool);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (rbAsync.Checked == true)
                AsyncSample();
            else if (rbWaitableAsync.Checked == true)
                WaitableAsyncSample();
            else if (rbUserCreatedThreadPool.Checked == true)
                UserCreatedThreadPoolSample();
            else if (rbWaitAllEx.Checked == true)
                WaitAllExSample();
            else if (rbExecutionCompletionDelegate.Checked == true)
                ExecutionCompletionDelegateSample();
            else if (rbDependentAsync.Checked == true)
                DependentAsyncSample();
            else if (rbDependentAsyncArray.Checked == true)
                DependentAsyncArraySample();
            else if (rbExceptionHandling.Checked == true)
                ExceptionHandlingSample();
            else if (rbWinFormsControlUpdate.Checked == true)
                WinFormsControlUpdateSample();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
        }

        private Sonic.Net.ThreadPool _myThreadPool = new Sonic.Net.ThreadPool(5, 2);
    }
}