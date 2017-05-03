﻿using System.IO;

namespace InstallationValidator.Core
{
   public static class Constants
   {
      public static readonly string ISSUE_TRACKER_URL = "https://github.com/Open-Systems-Pharmacology/InstallationValidator/issues";
      public static readonly string APPLICATION_FOLDER_PATH = @"Open Systems Pharmacology\InstallationValidator";
      public const double MAX_DEVIATION_TIME = 0.0001; //0.01%
      public const double MAX_DEVIATION_OUTPUT = 0.02; //2%
      public static readonly string PRODUCT_NAME = "Installation Validator";
      public static readonly string PRODUCT_NAME_WITH_TRADEMARK = $"{PRODUCT_NAME}®";
      public static readonly string DIMENSION_FILE = "OSPSuite.Dimensions.xml";
      public static readonly string DEFAULT_SKIN = "Metropolis"; //"Office 2013 Light Gray";
      public const int BUTTON_HEIGHT = 48;

      public static class Tools
      {
         public static readonly string PKSIM_BATCH_TOOL = "PKSim.BatchTool.exe";
         public static readonly string BATCH_INPUTS = Path.Combine("Inputs", "BatchFiles");
         public static readonly string BATCH_OUTPUTS = Path.Combine("Outputs", "BatchFiles");
         public static readonly string BATCH_LOG = "batch.log";
         public static readonly string TEX_TEMPLATES = "TexTemplates";
      }

      public static class Filter
      {
         //TODO MOVE TO CORE
         public static readonly string JSON_FILTER = $"*{OSPSuite.Core.Domain.Constants.Filter.JSON_EXTENSION}";
      }
   }
}