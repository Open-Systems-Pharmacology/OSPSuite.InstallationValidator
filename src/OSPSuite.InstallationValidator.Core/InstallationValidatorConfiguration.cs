using System;
using System.IO;
using Microsoft.Win32;
using OSPSuite.Assets;
using OSPSuite.Core;
using OSPSuite.Core.Domain;
using OSPSuite.Infrastructure.Configuration;
using OSPSuite.Utility;
using static OSPSuite.Core.Domain.Constants;

namespace OSPSuite.InstallationValidator.Core
{
   public interface IInstallationValidatorConfiguration : IApplicationConfiguration
   {
      string PKSimInstallFolderPath { get; }
      string BatchInputsFolderPath { get; }
      string BatchOutputsFolderPath { get; }
      string PKSimBatchToolPath { get; }
      string PKSimBinaryExecutablePath { get; }
      string MoBiBinaryExecutablePath { get; }
      string DimensionFilePath { get; }
   }

   public class InstallationValidatorConfiguration : OSPSuiteConfiguration, IInstallationValidatorConfiguration
   {
      protected override string ApplicationFolderPathWithRevision(string revision)
      {
         return Path.Combine(Constants.APPLICATION_FOLDER_PATH, revision);
      }

      protected override string[] LatestVersionWithOtherMajor { get; }
      public override string ChartLayoutTemplateFolderPath { get; }
      public override string TEXTemplateFolderPath => Path.Combine(applicationSettingsFolderPath, Constants.Tools.TEX_TEMPLATES);
      public override string ProductName => Constants.PRODUCT_NAME_WITH_TRADEMARK;
      public override Origin Product { get; }
      public override string ProductNameWithTrademark => Constants.PRODUCT_NAME_WITH_TRADEMARK;
      public override ApplicationIcon Icon { get; } = ApplicationIcons.Comparison;
      public override string UserSettingsFileName { get; }
      public override string IssueTrackerUrl { get; } = Constants.ISSUE_TRACKER_URL;
      private string applicationFolderPathWithMajorVersion => ApplicationFolderPathWithRevision(MajorVersion);
      private string applicationSettingsFolderPath => applicationSettingsFolderPathFor(applicationFolderPathWithMajorVersion);
      public string DimensionFilePath => createApplicationDataPathFor(Constants.DIMENSION_FILE);

      private string applicationSettingsFolderPathFor(string applicationFolderPath)
      {
         return Path.Combine(EnvironmentHelper.ApplicationDataFolder(), applicationFolderPath);
      }

      public string PKSimBinaryExecutablePath => getRegistryValueForRegistryPathAndKey(RegistryPaths.PKSIM_REG_PATH, RegistryPaths.INSTALL_PATH);

      public string MoBiBinaryExecutablePath => getRegistryValueForRegistryPathAndKey(RegistryPaths.MOBI_REG_PATH, RegistryPaths.INSTALL_PATH);

      public string PKSimInstallFolderPath => getRegistryValueForRegistryPathAndKey(RegistryPaths.PKSIM_REG_PATH, RegistryPaths.INSTALL_DIR);

      public string BatchInputsFolderPath => createApplicationDataPathFor(Constants.Tools.BATCH_INPUTS);

      public string BatchOutputsFolderPath => createApplicationDataPathFor(Constants.Tools.BATCH_OUTPUTS);

      public string PKSimBatchToolPath => Path.Combine(PKSimInstallFolderPath, Constants.Tools.PKSIM_BATCH_TOOL);

      private string createApplicationDataPathFor(string fileName) => Path.Combine(applicationSettingsFolderPath, fileName);

      private string getRegistryValueForRegistryPathAndKey(string openSystemsPharmacologyPKSim, string installDir)
      {
         try
         {
            return (string) Registry.GetValue($@"HKEY_LOCAL_MACHINE\SOFTWARE\{openSystemsPharmacologyPKSim}{MajorVersion}", installDir, null);
         }
         catch (Exception)
         {
            return string.Empty;
         }
      }
   }
}