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
open Microsoft.FSharp.Control.WebExtensions

module MathLibrary =
    let internal loadPrices ticker = async {
        let url = "http://real-chart.finance.yahoo.com/table.csv?s=" + ticker + "&d=10&e=20&f=2015&g=d&a=2&b=13&c=1986&ignore=.csv"

        let req = WebRequest.Create(url)
        let! resp = req.AsyncGetResponse()
        let stream = resp.GetResponseStream()
        let reader = new StreamReader(stream)
        let! csv = reader.AsyncReadToEnd()

        let prices =
            csv.Split([|'\n'|])
            |> Seq.skip 1
            |> Seq.map (fun line -> line.Split([|','|]))
            |> Seq.filter (fun values -> values |> Seq.length = 7)
            |> Seq.map (fun values ->
                System.DateTime.Parse(values.[0]),
                float values.[6])
        return prices }

    type StockAnalyzer (lprices, days) =
        let prices =
            lprices
            |> Seq.map snd
            |> Seq.take days
        static member GetAnalyzers (tickers, days) =
            tickers
            |> Seq.map loadPrices
            |> Async.Parallel
            |> Async.RunSynchronously
            |> Seq.map (fun prices -> new StockAnalyzer(prices, days))
        member s.Return =
            let lastPrice = prices |> Seq.item 0
            let startPrice = prices |> Seq.item (days - 1)
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

    open System.Windows.Forms

    let grid (prices : seq<System.DateTime * float>) =
        let form = new Form(Visible = true, TopMost = true)
        let grid = new DataGridView(Dock = DockStyle.Fill, Visible = true)
        form.Controls.Add(grid)
        grid.DataSource <- prices |> Seq.toArray