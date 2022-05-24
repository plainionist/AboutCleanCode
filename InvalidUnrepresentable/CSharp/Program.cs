using System;

namespace CSharp
{

    class CellValue
    {
        public static CellValue Id(int number, string kind, int version) => 
            new IdCell { Value = new Id { Number = number, Kind = kind, Version = version } };
        public static CellValue WildcardId(int number, string kind) => 
            new WildcardIdCell { Value = new WildcardId { Number = number, Kind = kind } };
        public static CellValue Empty() => new EmptyCell();
        public static CellValue FreeText(string text) => 
            new FreeTextCell { Value = new FreeText { Text = text } };

        private CellValue() {}

        private class IdCell : CellValue { public Id Value; }
        private class WildcardIdCell : CellValue { public WildcardId Value; }
        private class EmptyCell : CellValue { }
        private class FreeTextCell : CellValue { public FreeText Value; }

        public void Match(Action<Id> onId, Action<WildcardId> onWildcardId,
            Action onEmptyCell, Action<FreeText> onText)
        {
            if (this is IdCell id)
            {
                onId(id.Value);
            }
            else if (this is WildcardIdCell wildcard)
            {
                onWildcardId(wildcard.Value);
            }
            else if (this is EmptyCell empty)
            {
                onEmptyCell();
            }
            else if (this is FreeTextCell text)
            {
                onText(text.Value);
            }
            else
            {
                throw new NotSupportedException($"Unknown CellValue: {this.GetType()}");
            }
        }
        public T Select<T>(Func<Id, T> onId, Func<WildcardId, T> onWildcardId,
            Func<T> onEmptyCell, Func<FreeText, T> onText)
        {
            if (this is IdCell id)
            {
                return onId(id.Value);
            }
            else if (this is WildcardIdCell wildcard)
            {
                return onWildcardId(wildcard.Value);
            }
            else if (this is EmptyCell empty)
            {
                return onEmptyCell();
            }
            else if (this is FreeTextCell text)
            {
                return onText(text.Value);
            }
            else
            {
                throw new NotSupportedException($"Unknown CellValue: {this.GetType()}");
            }
        }
    }

    class Id
    {
        public int Number { get; init; }
        public string Kind { get; init; }
        public int Version { get; init; }
    }

    class WildcardId
    {
        public int Number { get; init; }
        public string Kind { get; init; }
    }

    class FreeText
    {
        public string Text { get; init; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Print(CellValue.Id(1234,"blub",1));
        }

        private static void Print(CellValue value)
        {
            value.Match(
                id => Console.WriteLine($"{id.Number}-{id.Kind}-{id.Version}"),
                wildcard => Console.WriteLine($"{wildcard.Number}-{wildcard.Kind}-*"),
                () => Console.WriteLine($"<empty>"),
                text => Console.WriteLine($"{text}")
            );
        }

        private Id TryResolve(Func<WildcardId, Id> resolve, CellValue value)
        {
            return value.Select(
                id => id,
                wildcardId => resolve(wildcardId),
                () => null,
                text => null
            );
        }
    }
}
