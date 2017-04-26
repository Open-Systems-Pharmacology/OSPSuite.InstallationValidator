﻿using System.Threading;
using FakeItEasy;
using NUnit.Framework;
using OSPSuite.BDDHelper;
using OSPSuite.InstallationValidator.Core;
using OSPSuite.InstallationValidator.Core.Domain;
using OSPSuite.InstallationValidator.Core.Services;
using OSPSuite.Utility.Events;

namespace OSPSuite.InstallationValidator.Services
{
   public abstract class concern_for_BatchStarterTask : ContextSpecification<BatchStarterTask>
   {
      protected IInstallationValidationConfiguration _installationValidationConfiguration;
      protected IStartableProcessFactory _startableProcessFactory;
      protected ILogWatcherFactory _logWatcherFactory;
      protected StartableProcess _startableProcess;
      protected ILogWatcher _logWatcher;

      protected override void Context()
      {
         _installationValidationConfiguration = A.Fake<IInstallationValidationConfiguration>();
         _startableProcessFactory = A.Fake<IStartableProcessFactory>();
         _logWatcherFactory = A.Fake<ILogWatcherFactory>();

         _logWatcher = A.Fake<ILogWatcher>();

         sut = new BatchStarterTask(_installationValidationConfiguration, _startableProcessFactory, _logWatcherFactory);
         _startableProcess = A.Fake<StartableProcess>();

         A.CallTo(() => _startableProcessFactory.CreateStartableProcess(A<string>._, A<string[]>._)).Returns(_startableProcess);
         A.CallTo(() => _logWatcherFactory.CreateLogWatcher(A<string>._)).Returns(_logWatcher);
      }
   }


   public class When_starting_a_validation : concern_for_BatchStarterTask
   {
      protected override void Because()
      {
         sut.StartBatch("./outputfolder", new CancellationToken()).Wait();
      }

      [Test]
      public void a_new_startable_process_is_created()
      {
         A.CallTo(() => _startableProcessFactory.CreateStartableProcess(A<string>._, A<string[]>._)).MustHaveHappened();
      }

      [Observation]
      public void a_file_watch_is_started()
      {
         A.CallTo(() => _logWatcher.Watch()).MustHaveHappened();
      }

      [Observation]
      public void the_startable_process_must_be_started()
      {
         A.CallTo(() => _startableProcess.Start()).MustHaveHappened();
         A.CallTo(() => _startableProcess.Wait(A<CancellationToken>._)).MustHaveHappened();
      }
   }
}
