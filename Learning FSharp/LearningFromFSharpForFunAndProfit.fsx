#light
//---------------------
// Section 'Why use F#?'
// http://fsharpforfunandprofit.com/why-use-fsharp/
module WhyUseFSharp =
    // one-liners
    [1..100] |> List.sum |> printfn "sum=%d"

    // no curly braces, semicolons or parentheses
    let square x = x * x
    let sq = square 42

    // simple types in one line
    type Person = {First:string; Last:string}

    // complex types in a few lines
    type Employee =
      | Worker of Person
      | Manager of Employee list

    // type inference
    let jdoe = {First="John";Last="Doe"}
    let worker = Worker jdoe

    // automatic equality and comparison
    type Person2 = {First:string; Last:string}
    let person1 = {First="john"; Last="Doe"}
    let person2 = {First="john"; Last="Doe"}
    printfn "Equal? %A"  (person1 = person2)

    // easy IDisposable logic with "use" keyword
    //use reader = new StreamReader()

    // easy composition of functions
    let add2times3 = (+) 2 >> (*) 3
    let result = add2times3 5

    // strict type checking
    //printfn "print string %s" 123 //compile error

    // all values immutable by default
    //person1.First <- "new name"  //assignment error

    // never have to check for nulls
    let makeNewString str =
       //str can always be appended to safely
       let newString = str + " new!"
       newString

    // embed business logic into types
    //emptyShoppingCart.remove   // compile error!

    // units of measure
    //let distance = 10<m> + 10<ft> // error!

    // easy async logic with "async" keyword
    //let! result = async {something}

    // easy parallelism
    //Async.Parallel [ for i in 0..40 ->
    //    async { return fib(i) } ]

    // message queues
    //MailboxProcessor.Start(fun inbox-> async{
    //    let! msg = inbox.Receive()
    //    printfn "message is: %s" msg
    //    })

    // impure code when needed
    let mutable counter = 0

    // create C# compatible classes and interfaces
    type IEnumerator<'a> =
        abstract member Current : 'a
        abstract MoveNext : unit -> bool

    // extension methods
    type System.Int32 with
        member this.IsEven = this % 2 = 0

    let i=20
    if i.IsEven then printfn "'%i' is even" i

    // UI code
    //open System.Windows.Forms
    //let form =
    //    new Form(
    //        Width= 400, Height = 300, Visible = true, Text = "Hello World"
    //        )
    //form.TopMost <- true
    //form.Click.Add (fun args-> printfn "clicked!")
    //form.Show()

//---------------------
// Section 'Why use F#?' Series no. 1
// http://fsharpforfunandprofit.com/posts/why-use-fsharp-intro/
// No Code

//---------------------
// Section 'Why use F#?' Series no. 2
// http://fsharpforfunandprofit.com/posts/fsharp-in-60-seconds/

module WhyUseFSharpSeries2 =
    // single line comments use a double slash
    (* multi line comments use (* . . . *) pair

    -end of multi line comment- *)

    // ======== "Variables" (but not really) ==========
    // The "let" keyword defines an (immutable) value
    let myInt = 5
    let myFloat = 3.14
    let myString = "hello"   //note that no types needed

    // ======== Lists ============
    let twoToFive = [2;3;4;5]        // Square brackets create a list with
                                     // semicolon delimiters.
    let oneToFive = 1 :: twoToFive   // :: creates list with new 1st element
    // The result is [1;2;3;4;5]
    let zeroToFive = [0;1] @ twoToFive   // @ concats two lists

    // IMPORTANT: commas are never used as delimiters, only semicolons!

    // ======== Functions ========
    // The "let" keyword also defines a named function.
    let square x = x * x          // Note that no parens are used.
    square 3                      // Now run the function. Again, no parens.

    let add x y = x + y           // don't use add (x,y)! It means something
                                  // completely different.
    add 2 3                       // Now run the function.

    // to define a multiline function, just use indents. No semicolons needed.
    let evens list =
       let isEven x = x%2 = 0     // Define "isEven" as an inner ("nested") function
       List.filter isEven list    // List.filter is a library function
                                  // with two parameters: a boolean function
                                  // and a list to work on

    evens oneToFive               // Now run the function

    // You can use parens to clarify precedence. In this example,
    // do "map" first, with two args, then do "sum" on the result.
    // Without the parens, "List.map" would be passed as an arg to List.sum
    let sumOfSquaresTo100 =
       List.sum ( List.map square [1..100] )

    // You can pipe the output of one operation to the next using "|>"
    // Here is the same sumOfSquares function written using pipes
    let sumOfSquaresTo100piped =
       [1..100] |> List.map square |> List.sum  // "square" was defined earlier

    // you can define lambdas (anonymous functions) using the "fun" keyword
    let sumOfSquaresTo100withFun =
       [1..100] |> List.map (fun x->x*x) |> List.sum

    // In F# returns are implicit -- no "return" needed. A function always
    // returns the value of the last expression used.

    // ======== Pattern Matching ========
    // Match..with.. is a supercharged case/switch statement.
    let simplePatternMatch =
       let x = "a"
       match x with
        | "a" -> printfn "x is a"
        | "b" -> printfn "x is b"
        | _ -> printfn "x is something else"   // underscore matches anything

    // Some(..) and None are roughly analogous to Nullable wrappers
    let validValue = Some(99)
    let invalidValue = None

    // In this example, match..with matches the "Some" and the "None",
    // and also unpacks the value in the "Some" at the same time.
    let optionPatternMatch input =
       match input with
        | Some i -> printfn "input is an int=%d" i
        | None -> printfn "input is missing"

    optionPatternMatch validValue
    optionPatternMatch invalidValue

    // ========= Complex Data Types =========

    // Tuple types are pairs, triples, etc. Tuples use commas.
    let twoTuple = 1,2
    let threeTuple = "a",2,true

    // Record types have named fields. Semicolons are separators.
    type Person = {First:string; Last:string}
    let person1 = {First="john"; Last="Doe"}

    // Union types have choices. Vertical bars are separators.
    type Temp =
        | DegreesC of float
        | DegreesF of float
    let temp = DegreesF 98.6

    // Types can be combined recursively in complex ways.
    // E.g. here is a union type that contains a list of the same type:
    type Employee =
      | Worker of Person
      | Manager of Employee list
    let jdoe = {First="John";Last="Doe"}
    let worker = Worker jdoe

    // ========= Printing =========
    // The printf/printfn functions are similar to the
    // Console.Write/WriteLine functions in C#.
    printfn "Printing an int %i, a float %f, a bool %b" 1 2.0 true
    printfn "A string %s, and something generic %A" "hello" [1;2;3;4]

    // all complex types have pretty printing built in
    printfn "twoTuple=%A,\nPerson=%A,\nTemp=%A,\nEmployee=%A"
             twoTuple person1 temp worker

    // There are also sprintf/sprintfn functions for formatting data
    // into a string, similar to String.Format.

//---------------------
// Section 'Why use F#?' Series no. 3
// http://fsharpforfunandprofit.com/posts/fvsc-sum-of-squares/
// define the square function
module WhyUseFSharpSeries3 =
    let square x = x * x

    // define the sumOfSquares function
    let sumOfSquares n =
       [1..n] |> List.map square |> List.sum

    // try it
    sumOfSquares 100

    let squareF x = x * x

    // define the sumOfSquares function
    let sumOfSquaresF n =
       [1.0 .. n] |> List.map squareF |> List.sum  // "1.0" is a float

    sumOfSquaresF 100.0

//---------------------
// Section 'Why use F#?' Series no. 4
// http://fsharpforfunandprofit.com/posts/fvsc-quicksort/
module WhyUseFSharpSeries4 =
    let rec quicksort list =
       match list with
       | [] ->                            // If the list is empty
            []                            // return an empty list
       | firstElem::otherElements ->      // If the list is not empty
            let smallerElements =         // extract the smaller ones
                otherElements
                |> List.filter (fun e -> e < firstElem)
                |> quicksort              // and sort them
            let largerElements =          // extract the large ones
                otherElements
                |> List.filter (fun e -> e >= firstElem)
                |> quicksort              // and sort them
            // Combine the 3 parts into a new list and return it
            List.concat [smallerElements; [firstElem]; largerElements]

    //test
    printfn "%A" (quicksort [1;5;23;18;9;1;3])

    let rec quicksort2 = function
       | [] -> []
       | first::rest ->
            let smaller,larger = List.partition ((>=) first) rest
            List.concat [quicksort2 smaller; [first]; quicksort2 larger]

    // test code
    printfn "%A" (quicksort2 [1;5;23;18;9;1;3])

//---------------------
// Section 'Why use F#?' Series no. 5
// http://fsharpforfunandprofit.com/posts/fvsc-download/
// "open" brings a .NET namespace into visibility
module WhyUseFSharpSeries5 =
    open System
    open System.IO
    open System.Net

    // Fetch the contents of a web page
    let fetchUrl callback url =
        let req = WebRequest.Create(Uri(url))
        use resp = req.GetResponse()
        use stream = resp.GetResponseStream()
        use reader = new IO.StreamReader(stream)
        callback reader url


    let myCallback (reader:IO.StreamReader) url =
        let html = reader.ReadToEnd()
        let html1000 = html.Substring(0,1000)
        printfn "Downloaded %s. First 1000 is %s" url html1000
        html      // return all the html

    //test
    let google = fetchUrl myCallback "http://google.com"

    // build a function with the callback "baked in"
    let fetchUrl2 = fetchUrl myCallback

    // test
    let google2 = fetchUrl2 "http://www.google.com"
    let bbc    = fetchUrl2 "http://news.bbc.co.uk"

    // test with a list of sites
    let sites = ["http://www.bing.com";
                 "http://www.google.com";
                 "http://www.yahoo.com"]

    // process each site in the list
    sites |> List.map fetchUrl2

//---------------------
// Section 'Why use F#?' Series no. 6
// http://fsharpforfunandprofit.com/posts/key-concepts/
module WhyUseFSharpSeries6 =
    let square x = x * x

    // functions as values
    let squareclone = square
    let result = [1..10] |> List.map squareclone

    // functions taking other functions as parameters
    let execFunction aFunc aParam = aFunc aParam
    let result2 = execFunction square 12


    //declare it
    type IntAndBool = {intPart: int; boolPart: bool}

    //use it
    let x = {intPart=1; boolPart=false}

    //declare it
    type IntOrBool =
        | IntChoice of int
        | BoolChoice of bool

    //use it
    let y = IntChoice 42
    let z = BoolChoice true

// if-like style
//    match booleanExpression with
//        true -> // true branch
//        false -> // false branch

// switch-like style
//    match aDigit with
//    | 1 -> // Case when digit=1
//    | 2 -> // Case when digit=2
//    | _ -> // Case otherwise

// loop-like style
//    match aList with
//    | [] ->
//         // Empty case
//    | first::rest ->
//         // Case with at least one element.
//         // Process first element, and then call
//         // recursively with the rest of the list

    type Shape =        // define a "union" of alternative structures
    | Circle of int
    | Rectangle of int * int
    | Polygon of (int * int) list
    | Point of (int * int)

    let draw shape =    // define a function "draw" with a shape param
      match shape with
      | Circle radius ->
          printfn "The circle has a radius of %d" radius
      | Rectangle (height,width) ->
          printfn "The rectangle is %d high by %d wide" height width
      | Polygon points ->
          printfn "The polygon is made of these points %A" points
      | _ -> printfn "I don't recognize this shape"

    let circle = Circle(10)
    let rect = Rectangle(4,5)
    let polygon = Polygon( [(1,1); (2,2); (3,3)])
    let point = Point(2,3)

    [circle; rect; polygon; point] |> List.iter draw

//---------------------
// Section 'Why use F#?' Series no. 7
// http://fsharpforfunandprofit.com/posts/conciseness-intro/


//---------------------
// Section 'Why use F#?' Series no. 8
//---------------------
// Section 'Why use F#?' Series no. 9
//---------------------
// Section 'Why use F#?' Series no. 10