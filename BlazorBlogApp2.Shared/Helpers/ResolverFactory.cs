namespace BlazorBlogApp2.Shared
{
    using System;
    using System.Threading.Tasks;

    public static class ResolverFactory
    {
        public static T CreateInstance<T>(string typeName)
            where T : class
        {

            Type type = Type.GetType(typeName);

            T instance = (T)Activator.CreateInstance(type);

            return instance;
        }

        public static T CreateInstance<T>(string typeName, params object[] args)
            where T : class
        {

            Type type = Type.GetType(typeName);

            T instance = (T)Activator.CreateInstance(type, args);

            return instance;
        }

        public static object CreateInstance(Type type, params object[] args)
        {
            return Activator.CreateInstance(type, args);
        }

        public static T GetPropValue<T>(this object src, string propName)
        {
            return (T)src.GetType().GetProperty(propName).GetValue(src, null);
        }

        public static object GetPropValue(this object src, string propName)
        {
            return src.GetType().GetProperty(propName).GetValue(src, null);
        }

        public static Type GetType(this object src, string propName)
        {
            try
            {
                return src.GetType().GetProperty(propName).PropertyType;
            }
            catch
            {
                return null;
            }
        }

        public static void SetPropValue<TModel>(this TModel src, string propName, object val)
        {
            if (src.GetType().GetProperty(propName) != null)
            {
                src.GetType().GetProperty(propName).SetValue(src, val);
            }
        }

        public static void Invoke(this object src, string methodName, object[] param)
        {
            src.GetType().GetMethod(methodName).Invoke(src, param);
        }

        public static void Invoke(this object src, string methodName, Type[] types, object[] param)
        {
            src.GetType()
                .GetMethod(methodName, types)
                .Invoke(src, param);
        }

        public static Task<TModel> InvokeAsync<TModel>(this object src, string methodName, object[] param)
        {
            return (Task<TModel>)src.GetType().GetMethod(methodName).Invoke(src, param);
        }

        public static Task InvokeAsync(this object src, string methodName, Type[] types, object[] param)
        {
            return (Task)src.GetType().GetMethod(methodName, types).Invoke(src, param);
        }
    }
}
