using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AvoidUsingAsyncVoidTests
{
    public static class AssertExtensions
    {
        public static void AssertNoAsyncVoidMethods(Assembly assembly) {
            var messages = assembly
                .GetAsyncVoidMethods()
                .Select( method => $"'{method.DeclaringType?.Name}.{method.Name}' is an async void method." )
                .ToList();
            Assert.IsFalse( messages.Any(),
                "Async void methods found!" + Environment.NewLine + string.Join( Environment.NewLine, messages ) );
        }

        private static IEnumerable<MethodInfo> GetAsyncVoidMethods(this Assembly assembly) {
            return assembly.GetLoadableTypes()
                .SelectMany( type => type.GetMethods(
                     BindingFlags.NonPublic
                     | BindingFlags.Public
                     | BindingFlags.Instance
                     | BindingFlags.Static
                     | BindingFlags.DeclaredOnly ) )
                .Where( method => method.HasAttribute<AsyncStateMachineAttribute>() )
                .Where( method => method.ReturnType == typeof( void ) );
        }

        private static IEnumerable<Type> GetLoadableTypes(this Assembly assembly) {
            if( assembly == null ) throw new ArgumentNullException( nameof( assembly ) );
            try {
                return assembly.GetTypes();
            }
            catch( ReflectionTypeLoadException e ) {
                return e.Types.Where( t => t != null );
            }
        }

        private static bool HasAttribute<TAttribute>(this MethodInfo method) where TAttribute : Attribute {
            return method.GetCustomAttributes( typeof( TAttribute ), false ).Any();
        }

        //If you uncomment this, then the below test will start to fail.
        //public static async void Foo()
        //{
        //	await Task.Run( () => { } );
        //}
    }

    [TestClass]
    public class Tests
    {
        [TestMethod]
        public void EnsureNoAsyncVoidTests() {
            AssertExtensions.AssertNoAsyncVoidMethods( GetType().Assembly );

            var assemblies =
                AppDomain.CurrentDomain.GetAssemblies()
                    .Where( a => a.FullName.Contains( "Version=1." ) || a.FullName.Contains( "Version=0." ) )
                    .ToList();
            foreach( var assembly in assemblies )
                AssertExtensions.AssertNoAsyncVoidMethods( assembly );
        }

        //If you uncomment this, then the above test will start to fail.
        //[TestMethod]
        //public async void Foo()
        //{
        //	await Task.Run( () => { } );
        //}
    }
}