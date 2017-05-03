using System.Collections.Generic;
using System.Linq;
using InstallationValidator.Core.Domain;
using OSPSuite.Infrastructure.Reporting;
using OSPSuite.TeXReporting.Builder;

namespace InstallationValidator.Core.Reporting
{
   public class OutputFileComparisonResultTeXBuilder : FileComparisonResultTeXBuilder<OutputFileComparisonResult>
   {
      public OutputFileComparisonResultTeXBuilder(ITeXBuilderRepository builderRepository) : base(builderRepository)
      {
      }

      public override void Build(OutputFileComparisonResult fileComparisonResult, OSPSuiteTracker buildTracker)
      {
         base.Build(fileComparisonResult, buildTracker);
         var comparisonResults = new List<ValueComparisonResult>();

         comparisonResults.AddRange(fileComparisonResult.OutputComparisonResults.Where(x => !x.IsStateValid()));

         if (!fileComparisonResult.TimeComparison.IsStateValid())
            comparisonResults.Add(fileComparisonResult.TimeComparison);

         _builderRepository.Report(comparisonResults, buildTracker);
      }
   }
}