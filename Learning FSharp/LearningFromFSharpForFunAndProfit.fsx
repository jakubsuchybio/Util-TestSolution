#light
//---------------------
//Section 'Why use F#?'
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
//Section 'Why use F#?' Series no. 1
module WhyUseFSharpSeries1 =









//---------------------
//Section 'Why use F#?' Series no. 2
//---------------------
//Section 'Why use F#?' Series no. 3
//---------------------
//Section 'Why use F#?' Series no. 4
//---------------------
//Section 'Why use F#?' Series no. 5
//---------------------
//Section 'Why use F#?' Series no. 6
//---------------------
//Section 'Why use F#?' Series no. 7
//---------------------
//Section 'Why use F#?' Series no. 8
//---------------------
//Section 'Why use F#?' Series no. 9
//---------------------
//Section 'Why use F#?' Series no. 10