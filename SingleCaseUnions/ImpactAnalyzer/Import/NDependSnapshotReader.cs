using System.Collections.Generic;
using System.Linq;
using ImpactAnalyzer.Entities;
using NDepend.CodeModel;

namespace ImpactAnalyzer.Import
{
    class NDependSnapshotReader
    {
        public IEnumerable<CodeType> GetApplicationTypes(ICodeBase codeBase) =>
            codeBase.Application.Types
                .Where(t => !ShouldAssemblyBeConsidered(t))
                .Select(CreateCodeType)
                .ToList();


        private CodeType CreateCodeType(IType type)
        {
            return new CodeType(
                type.ParentNamespace.FullName,
                type.Name,
                type.ParentAssembly.FullName);
        }


        public IEnumerable<CodeType> GetAppThirdTypes(ICodeBase codeBase) =>
            codeBase.Types
                .Where(t => t.IsThirdParty)
                .Where(t => !ShouldAssemblyBeConsidered(t))
                .Select(CreateCodeType)
                .ToList();

        private bool ShouldAssemblyBeConsidered(IType t)
        {
            // TODO: black listing of irrelevant types
            return true;
        }
    }
}
