﻿using System.Collections.Generic;
using InstallationValidator.Core.Assets;
using OSPSuite.Core.Domain;
using OSPSuite.Utility.Extensions;

namespace InstallationValidator.Core.Domain
{
   public class BatchComparisonResult : IWithValidationState
   {
      private readonly List<FileComparisonResult> _fileComparisonResults = new List<FileComparisonResult>();

      /// <summary>
      ///    First folder used for comparison
      /// </summary>
      public string FolderPath1 { get; set; }

      /// <summary>
      ///    Second folder used for comparison
      /// </summary>
      public string FolderPath2 { get; set; }


      public string FolderPathCaption1 { get; set; } = Captions.Old;


      public string FolderPathCaption2 { get; set; } = Captions.New;

      public void AddFileComparisons(IEnumerable<FileComparisonResult> fileComparisonResults)
      {
         fileComparisonResults.Each(AddFileComparison);
      }

      public void AddFileComparison(FileComparisonResult fileComparisonResult)
      {
         _fileComparisonResults.Add(fileComparisonResult);
      }

      public IReadOnlyList<FileComparisonResult> FileComparisonResults => _fileComparisonResults;

      public ValidationState State => _fileComparisonResults.CombineStates();
   }
}