using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connectors.AI.Bedrock.Helper
{
    public class MinimalContainer
    {
        private static Dictionary<Type, object> MyContainers = new Dictionary<Type, object>();

        public static void Register<T>(T data)
        {
            if(!MyContainers.ContainsKey(typeof(T))) 
                MyContainers.Add(typeof(T), data);
        }

        public static T Get<T>()
        {
            return (T)MyContainers[typeof(T)];
        }

    }
}
