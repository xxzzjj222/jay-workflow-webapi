using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Jay.Workflow.WebApi.Common.Utils
{
    /// <summary>
    /// 程序集帮助类
    /// </summary>
    public static class AssemblyHelper
    {
        /// <summary>
        /// 缓存符合搜索条件的程序集信息
        /// </summary>
        private static readonly ConcurrentDictionary<string, List<Assembly>> AssembliesDict = new ConcurrentDictionary<string, List<Assembly>>();

        /// <summary>
        /// 获取程序集集合
        /// 根据搜索条件
        /// </summary>
        /// <param name="searchPattern"></param>
        /// <returns></returns>
        public static List<Assembly> GetAssemblies(string searchPattern)
        {
            if (!AssembliesDict.TryGetValue(searchPattern, out List<Assembly> assemblies))
            {
                assemblies = Directory.GetFiles(AppContext.BaseDirectory, searchPattern, SearchOption.AllDirectories)
                                      .Select(Assembly.LoadFrom)
                                      .Distinct()
                                      .ToList();

                AssembliesDict.TryAdd(searchPattern, assemblies);
            }

            return assemblies;
        }
    }
}
