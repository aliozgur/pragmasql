namespace Teroid.DataGridViewToolStrip
{
    using System;

    internal class UserAction
    {
        private int m_ColumnIndex;
        private object m_NewValue;
        private object m_OldValue;
        private int m_RowIndex;
        private UserActionTypes m_UserActionType;

        internal UserAction(UserActionTypes type, int columnindex, int rowindex, object oldvalue, object newvalue)
        {
            this.m_UserActionType = type;
            this.m_ColumnIndex = columnindex;
            this.m_RowIndex = rowindex;
            this.m_OldValue = oldvalue;
            this.m_NewValue = newvalue;
        }

        public int ColumnIndex
        {
            get
            {
                return this.m_ColumnIndex;
            }
        }

        public object NewValue
        {
            get
            {
                return this.m_NewValue;
            }
        }

        public object OldValue
        {
            get
            {
                return this.m_OldValue;
            }
        }

        public int RowIndex
        {
            get
            {
                return this.m_RowIndex;
            }
        }

        public UserActionTypes UserActionType
        {
            get
            {
                return this.m_UserActionType;
            }
        }

        internal enum UserActionTypes
        {
            ValueChanged
        }
    }
}

