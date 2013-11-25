using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace PragmaSQL.Core
{
  public class CommonException : Exception
  {
    public CommonException(string message) : base(message) { }
    public CommonException(string message, Exception innerException) : base(message, innerException) { }
  }

  public class InvalidConnectionState:CommonException
  {

		public InvalidConnectionState(ConnectionState state) : base(String.Format("Connection is in state \"{0}\".", state)) { }
		public InvalidConnectionState(string message) : base(message) { }
    public InvalidConnectionState(string message, Exception innerException) : base(message, innerException) { }
  }
  
  public class ObjectTypeNotSupportedByOperation : CommonException
  {
    public ObjectTypeNotSupportedByOperation(string message) : base(message) { }
    public ObjectTypeNotSupportedByOperation(string message, Exception innerException) : base(message, innerException) { }
  }

  public class NullParameterException : CommonException
  {
    public NullParameterException(string message) : base(message) { }
    public NullParameterException(string message, Exception innerException) : base(message, innerException) { }
  }
  
  public class NullPropertyException: CommonException
  {
    public NullPropertyException(string message) : base(message) { }
    public NullPropertyException(string message, Exception innerException) : base(message, innerException) { }
  }
  
  public class InvalidConfiguration : CommonException
  {
    public InvalidConfiguration(string message) : base(message) { }
    public InvalidConfiguration(string message, Exception innerException) : base(message, innerException) { }
  }

  public class InvalidTypeException : CommonException
  {
    public InvalidTypeException(string message) : base(message) { }
    public InvalidTypeException(string message, Exception innerException) : base(message, innerException) { }
  }

  public class SelfReferenceException: CommonException
  {
    public SelfReferenceException(string message) : base(message) { }
    public SelfReferenceException(string message, Exception innerException) : base(message, innerException) { }
  }
  
  public class ItemNotInCollectionException: CommonException
  {
    public ItemNotInCollectionException(string message) : base(message) { }
    public ItemNotInCollectionException(string message, Exception innerException) : base(message, innerException) { }
  }
  
  internal class PersonalEditionLimitation : CommonException
  {
    public PersonalEditionLimitation(string message) : base(message) { }
    public PersonalEditionLimitation(string message, Exception innerException) : base(message, innerException) { }
    public PersonalEditionLimitation() : base("Personal Edition does not support this operation.\r\nPlease upgrade to Professional Edition from www.pragmasql.com.") { }
  }
}
