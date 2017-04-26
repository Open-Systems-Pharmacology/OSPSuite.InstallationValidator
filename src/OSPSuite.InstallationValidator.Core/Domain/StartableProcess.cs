﻿using System;
using System.Diagnostics;
using System.Threading;
using Microsoft.Win32.SafeHandles;

namespace OSPSuite.InstallationValidator.Core.Domain
{
   //TODO - move class to core
   public class StartableProcess : IDisposable
   {
      private readonly Process _process;
      private bool _exited;

      public StartableProcess(string filePath, params string[] arguments)
      {
         _process = new Process
         {
            StartInfo = new ProcessStartInfo
            {
               FileName = filePath,
               Arguments = string.Join(" ", arguments)
            }
         };
      }

      /// <summary>
      /// <returns>true</returns> if the process exited normally. If the process was stopped, returns false
      /// </summary>
      public bool ExitedNormally => _exited;

      /// <summary>
      /// <returns>the exit code of the process</returns> if <see cref="ExitedNormally"/> returns true.
      /// throws an <seealso cref="InvalidOperationException"/> if the process has not exited or was aborted abnormally
      /// </summary>
      public int ReturnCode => _process.ExitCode;

      public virtual void Start()
      {
         _process?.Start();
      }

      public void Dispose()
      {
         _process.Dispose();
      }

      public virtual void Wait(CancellationToken token)
      {
         using (var waitHandle = new SafeWaitHandle(_process.Handle, false))
         {
            using (var processFinishedEvent = new ManualResetEvent(false))
            {
               processFinishedEvent.SafeWaitHandle = waitHandle;

               var index = WaitHandle.WaitAny(new[] { processFinishedEvent, token.WaitHandle });
               if (index == 0)
               {
                  _exited = true;
                  return;
               }
               _process.Kill();
            }
         }
      }
   }
}