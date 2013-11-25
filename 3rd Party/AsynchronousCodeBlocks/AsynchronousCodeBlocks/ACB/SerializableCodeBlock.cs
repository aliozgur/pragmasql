using System;
using System.Collections;

namespace AsynchronousCodeBlocks
{
    [Serializable()]
    public struct FieldValue
    {
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
            }
        }

        public object Value
        {
            get
            {
                return _value;
            }
            set
            {
                _value = value;
            }
        }

        private string _name;
        private object _value;
    }

    [Serializable()]
    public class SerializableCodeBlock
    {
        public string TypeName
        {
            get
            {
                return _typeName;
            }
            set
            {
                _typeName = value;
            }
        }

        public string MethodName
        {
            get
            {
                return _methodName;
            }
            set
            {
                _methodName = value;
            }
        }

        public FieldValue[] FieldValueList
        {
            get
            {
                return _fieldValueList;
            }
            set
            {
                _fieldValueList = value;
            }
        }

        public byte[] MethodIL
        {
            get
            {
                return _methodIL;
            }
            set
            {
                _methodIL = value;
            }
        }

        public string ThreadPoolID
        {
            get
            {
                return _threadPoolID;
            }
            set
            {
                _threadPoolID = value;
            }
        }

        public int InstanceID
        {
            get
            {
                return _instanceID; 
            }
            set
            {
                _instanceID = value;
            }
        }

        private string _typeName = null;
        private string _methodName = null;
        private byte[] _methodIL = null;
        private FieldValue[] _fieldValueList = null;
        private string _threadPoolID = null;
        private int _instanceID = 0;
    }
}
