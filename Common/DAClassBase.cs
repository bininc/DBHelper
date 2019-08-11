using CommLiby;
using System;
using System.Linq.Expressions;
using DBHelper.BaseHelper;

namespace DBHelper.Common
{
    public class DAClassBase<T, Tmodel> : ClassBase<T> where Tmodel : class, new() where T : class, new()
    {
        private Type modelType;
        /// <summary>
        /// 获得Model类表名
        /// </summary>
        protected string ModelName
        {
            get
            {
                if (modelType == null)
                    modelType = typeof(Tmodel);
                return modelType.Name;
            }
        }

        /// <summary>
        /// 获得model类指定列名
        /// </summary>
        /// <typeparam name="Tmodel"></typeparam>
        /// <param name="exp"></param>
        /// <returns></returns>
        protected string FieldName(Expression<Func<Tmodel, object>> exp)
        {
            return Common_liby.FieldName(exp);
        }

        private static IDBHelper _dbHelper = null;
        public static IDBHelper dbHelper
        {
            get
            {
                if (_dbHelper == null)
                    _dbHelper = DBHelper.GetDBHelper();
                return _dbHelper;
            }
        }
    }
}

