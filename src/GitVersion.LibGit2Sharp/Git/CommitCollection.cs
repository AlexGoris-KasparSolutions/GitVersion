using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using LibGit2Sharp;

namespace GitVersion
{
    internal sealed class CommitCollection : ICommitCollection
    {
        private readonly ICommitLog innerCollection;
        internal CommitCollection(ICommitLog collection) => innerCollection = collection;

        public IEnumerator<ICommit> GetEnumerator()
        {
            return innerCollection.Select(commit => new Commit(commit)).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public IEnumerable<ICommit> GetCommitsPriorTo(DateTimeOffset olderThan) => this.SkipWhile(c => c.When > olderThan);
        public ICommitCollection QueryBy(CommitFilter commitFilter)
        {
            static object? GetReacheableFrom(object item)
            {
                return item switch
                {
                    Commit c => (LibGit2Sharp.Commit)c,
                    Branch b => (LibGit2Sharp.Branch)b,
                    _ => null
                };
            }

            var includeReachableFrom = GetReacheableFrom(commitFilter.IncludeReachableFrom);
            var excludeReachableFrom = GetReacheableFrom(commitFilter.ExcludeReachableFrom);
            var filter = new LibGit2Sharp.CommitFilter
            {
                IncludeReachableFrom = includeReachableFrom,
                ExcludeReachableFrom = excludeReachableFrom,
                FirstParentOnly = commitFilter.FirstParentOnly,
                SortBy = (LibGit2Sharp.CommitSortStrategies)commitFilter.SortBy,
            };
            var commitLog = ((IQueryableCommitLog)innerCollection).QueryBy(filter);
            return new CommitCollection(commitLog);
        }
    }
}
