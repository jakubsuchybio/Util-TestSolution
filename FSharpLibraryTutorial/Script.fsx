#light

namespace FSharpLibraryTutorial

module Seq =
    open System
    open System.Linq

    let pmap f (l:seq<_>) =
        let pe = l.AsParallel<_>()
        ParallelEnumerable.Select(pe, Func<_,_>(f))
    let psum (l:seq<_>) =
        let pe = l.AsParallel<float>()
        ParallelEnumerable.Sum(pe)

open System.Net
open System.IO

let loadPrices ticker =
    let url = "http://real-chart.finance.yahoo.com/table.csv?s=" + ticker + "&d=10&e=20&f=2015&g=d&a=2&b=13&c=1986&ignore=.csv"

    let req = WebRequest.Create(url)
    let resp = req.GetResponse()
    let stream = resp.GetResponseStream()
    let reader = new StreamReader(stream)
    let csv = reader.ReadToEnd()

    let prices =
        csv.Split([|'\n'|])
        |> Seq.skip 1
        |> Seq.map (fun line -> line.Split([|','|]))
        |> Seq.filter (fun values -> values |> Seq.length = 7)
        |> Seq.map (fun values ->
            System.DateTime.Parse(values.[0]),
            float values.[6])
    prices

type StockAnalyzer (lprices, days) =
    let prices =
        lprices
        |> Seq.map snd
        |> Seq.take days
    static member GetAnalyzers (tickers, days) =
        tickers
        |> Seq.map loadPrices
        |> Seq.map (fun prices -> new StockAnalyzer(prices, days))
    member s.Return =
        let lastPrice = prices |> Seq.nth 0
        let startPrice = prices |> Seq.nth (days - 1)
        lastPrice / startPrice - 1.
    member s.StdDev =
        let logRets =
            prices
            |> Seq.pairwise
            |> Seq.map (fun (x, y) -> log (x / y))
        let mean = logRets |> Seq.average
        let sqr x = x * x
        let var = logRets |> Seq.averageBy (fun r -> sqr (r - mean))
        sqrt var