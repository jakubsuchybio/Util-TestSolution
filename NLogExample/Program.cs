using System;
using NLog;
using NLog.Fluent;

//More info here: http://www.codeproject.com/Tips/1052902/How-to-NLog-with-VisualStudio
//Github source: https://github.com/NLog

namespace NLogExample
{
    public class Program
    {
        /*
		 * NLog tutorial suggests to use a static Logger, so here it is
		 */
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        private static void FluentMethod() {
            // From the tutorial
            Logger.Info()
                .Message( "This is a test fluent message '{0}'.", DateTime.Now.Ticks )
                .Property( "Test", "InfoWrite" )
                .Write();

            // Simple example of using a conditional write
            for( var i = 0; i < 5; i++ ) {
                Logger.Trace()
                    .Message( "you'll only see this if `i` is a multiple of 2. i = {0}", i )
                    .Property( "Test", "boom" )
                    .WriteIf( i++ % 2 == 0 );
            }
        }

        private static void Main(string[] args) {
            /*
			 * Welcome to this NLog demo
			 */
            Console.Out.WriteLine( "Greetings, some loggings is about to take place." );
            Console.Out.WriteLine( "" );

            Console.Out.WriteLine( "Let's assume you're going to work, and using the bus to get there:" );
            Console.Out.WriteLine( "------------------------------------------------------------------" );
            Logger.Trace( "Trace: The chatter of people on the street" );
            Logger.Debug( "Debug: Where are you going and why?" );
            Logger.Info( "Info: What bus station you're at." );
            Logger.Warn( "Warn: You're playing on the phone and not looking up for your bus" );
            Logger.Error( "Error: You get on the wrong bus." );
            Logger.Fatal( "Fatal: You are run over by the bus." );

            FluentMethod();

            /*
			 * Closing app
			 */
            Console.Out.WriteLine( "" );
            Console.Out.WriteLine( "Done logging." );
            Console.Out.WriteLine( "Hit any key to exit" );
            Console.ReadKey();
        }
    }
}