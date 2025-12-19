using System.Diagnostics.CodeAnalysis;

namespace Project.Reports
{
    public record Report<T>
    {
        public required DateTime GeneratedAt { get; init; }

        public required T Details { get; init; }

        public Report() { }

        [SetsRequiredMembers]
        public Report(T details) : this(DateTime.Now, details) { }

        [SetsRequiredMembers]
        public Report(DateTime generatedAt, T details)
        {
            GeneratedAt = generatedAt;
            Details = details;
        }
    }
}
