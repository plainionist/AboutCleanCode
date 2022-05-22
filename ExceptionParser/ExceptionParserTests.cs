using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace CodeOverComments
{
    public class ExceptionParserTests
    {
        [Test]
        public void SimpleException()
        {
            var exceptionText = @"
 System.InvalidOperationException: Dot plain file generation failed: Error: C:\Users\stanley\AppData\Local\Temp\be462a67-5731-42fb-9f72-706bbcee8371.dot: syntax error in line 7 near '.'

 Context: 
   System.Object: 
 StackTrace:    
   at Plainion.GraphViz.Dot.DotToDotPlainConverter.Convert(FileInfo dotFile, FileInfo plainFile)
   at Plainion.GraphViz.Dot.DotToDotPlainConverter.Convert(FileInfo dotFile, FileInfo plainFile)
   at Plainion.GraphViz.Dot.DotToolLayoutEngine.Relayout(IGraphPresentation presentation)
   at Plainion.GraphViz.Visuals.GraphVisual.Refresh()
   at Plainion.GraphViz.GraphView.OnIdle(Object sender, EventArgs e)
   at System.Windows.Interop.ComponentDispatcherThread.RaiseIdle()
   at System.Windows.Threading.Dispatcher.WndProcHook(IntPtr hwnd, Int32 msg, IntPtr wParam, IntPtr lParam, Boolean& handled)
   at MS.Win32.HwndWrapper.WndProc(IntPtr hwnd, Int32 msg, IntPtr wParam, IntPtr lParam, Boolean& handled)
   at MS.Win32.HwndSubclass.DispatcherCallbackOperation(Object o)
   at System.Windows.Threading.ExceptionWrapper.InternalRealCall(Delegate callback, Object args, Int32 numArgs)
   at System.Windows.Threading.ExceptionWrapper.TryCatchWhen(Object source, Delegate callback, Object args, Int32 numArgs, Delegate catchHandler)
            ";

            var parser = new ExceptionParser();
            var info = parser.Parse(exceptionText);

            Assert.That(info.ExceptionType, Is.EqualTo("System.InvalidOperationException"));
            Assert.That(info.Message, Does.StartWith("Dot plain file generation failed: "));
            Assert.That(info.StackTrace[0], Is.EqualTo("Plainion.GraphViz.Dot.DotToDotPlainConverter.Convert"));

            PrintWordCloud(info.WordCloud);
        }

        private void PrintWordCloud(IReadOnlyDictionary<string, int> wordCloud)
        {
            foreach (var entry in wordCloud)
            {
                Console.WriteLine($"{entry.Key}: {entry.Value}");
            }
        }

        [Test]
        public void WithInnerException()
        {
            var exceptionText = @"System.AggregateException: One or more errors occurred. (LiteDB ENSURE: empty page must be defined as empty type) 
---> System.Exception: LiteDB ENSURE: empty page must be defined as empty type
  at LiteDB.Constants.ENSURE (System.Boolean conditional, System.String message) [0x0002a] in <8e1f39e9bcf3452cab277acc385e2e12>:0 
  at LiteDB.Engine.Snapshot.NewPage[T] () [0x000a3] in <8e1f39e9bcf3452cab277acc385e2e12>:0 
  at LiteDB.Engine.Snapshot.GetFreeDataPage (System.Int32 bytesLength) [0x00081] in <8e1f39e9bcf3452cab277acc385e2e12>:0 
  at LiteDB.Engine.DataService+<>c__DisplayClass3_0+<<Insert>g__source|0>d.MoveNext () [0x00052] in <8e1f39e9bcf3452cab277acc385e2e12>:0 
  at LiteDB.Engine.BufferWriter..ctor (System.Collections.Generic.IEnumerable`1[T] source) [0x00029] in <8e1f39e9bcf3452cab277acc385e2e12>:0 
  at LiteDB.Engine.DataService.Insert (LiteDB.BsonDocument doc) [0x0005d] in <8e1f39e9bcf3452cab277acc385e2e12>:0 
  at LiteDB.Engine.LiteEngine.InsertDocument (LiteDB.Engine.Snapshot snapshot, LiteDB.BsonDocument doc, LiteDB.BsonAutoId autoId, LiteDB.Engine.IndexService indexer, LiteDB.Engine.DataService data) [0x00094] in <8e1f39e9bcf3452cab277acc385e2e12>:0 
  at LiteDB.Engine.LiteEngine+<>c__DisplayClass7_0.<Insert>b__0 (LiteDB.Engine.TransactionService transaction) [0x00076] in <8e1f39e9bcf3452cab277acc385e2e12>:0 
  at LiteDB.Engine.LiteEngine.AutoTransaction[T] (System.Func`2[T,TResult] fn) [0x00055] in <8e1f39e9bcf3452cab277acc385e2e12>:0 
  at LiteDB.Engine.LiteEngine.Insert (System.String collection, System.Collections.Generic.IEnumerable`1[T] docs, LiteDB.BsonAutoId autoId) [0x00055] in <8e1f39e9bcf3452cab277acc385e2e12>:0 
  at LiteDB.LiteCollection`1[T].Insert (T entity) [0x0002e] in <8e1f39e9bcf3452cab277acc385e2e12>:0 
   --- End of inner exception stack trace ---
---> (Inner Exception #0) System.Exception: LiteDB ENSURE: empty page must be defined as empty type
  at LiteDB.Constants.ENSURE (System.Boolean conditional, System.String message) [0x0002a] in <8e1f39e9bcf3452cab277acc385e2e12>:0 
  at LiteDB.Engine.Snapshot.NewPage[T] () [0x000a3] in <8e1f39e9bcf3452cab277acc385e2e12>:0 
  at LiteDB.Engine.Snapshot.GetFreeDataPage (System.Int32 bytesLength) [0x00081] in <8e1f39e9bcf3452cab277acc385e2e12>:0 
  at LiteDB.Engine.DataService+<>c__DisplayClass3_0+<<Insert>g__source|0>d.MoveNext () [0x00052] in <8e1f39e9bcf3452cab277acc385e2e12>:0 
  at LiteDB.Engine.BufferWriter..ctor (System.Collections.Generic.IEnumerable`1[T] source) [0x00029] in <8e1f39e9bcf3452cab277acc385e2e12>:0 
  at LiteDB.Engine.DataService.Insert (LiteDB.BsonDocument doc) [0x0005d] in <8e1f39e9bcf3452cab277acc385e2e12>:0 
  at LiteDB.Engine.LiteEngine.InsertDocument (LiteDB.Engine.Snapshot snapshot, LiteDB.BsonDocument doc, LiteDB.BsonAutoId autoId, LiteDB.Engine.IndexService indexer, LiteDB.Engine.DataService data) [0x00094] in <8e1f39e9bcf3452cab277acc385e2e12>:0 
  at LiteDB.Engine.LiteEngine+<>c__DisplayClass7_0.<Insert>b__0 (LiteDB.Engine.TransactionService transaction) [0x00076] in <8e1f39e9bcf3452cab277acc385e2e12>:0 
  at LiteDB.Engine.LiteEngine.AutoTransaction[T] (System.Func`2[T,TResult] fn) [0x00055] in <8e1f39e9bcf3452cab277acc385e2e12>:0 
  at LiteDB.Engine.LiteEngine.Insert (System.String collection, System.Collections.Generic.IEnumerable`1[T] docs, LiteDB.BsonAutoId autoId) [0x00055] in <8e1f39e9bcf3452cab277acc385e2e12>:0 
  at LiteDB.LiteCollection`1[T].Insert (T entity) [0x0002e] in <8e1f39e9bcf3452cab277acc385e2e12>:0 
";

            var parser = new ExceptionParser();
            var info = parser.Parse(exceptionText);

            Assert.That(info.ExceptionType, Is.EqualTo("System.Exception"));
            Assert.That(info.Message, Does.StartWith("LiteDB ENSURE: empty page must be defined as empty type"));
            Assert.That(info.StackTrace[0], Is.EqualTo("LiteDB.Constants.ENSURE"));

            PrintWordCloud(info.WordCloud);
        }
    }
}