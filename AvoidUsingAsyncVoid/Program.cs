namespace AvoidUsingAsyncVoid
{
    public class Program
    {
        private static void Main(string[] args) {
        }

        //If you uncomment this, then the test 'EnsureNoAsyncVoidTests' will start to fail.
        //public static async void Foo()
        //{
        //	await Task.Run( () => { } );
        //}
    }
}