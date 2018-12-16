using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NBench;
using ProjectManagerService.Controllers;

namespace ProjectManagerLoadTesting
{
    public class LoadTest
    {
        private HomeController _ctrl;
        private int index = 10;

        [PerfSetup]
        public void Setup(BenchmarkContext context)
        {
            _ctrl = new HomeController();
        }

        [PerfBenchmark(Description = "Test to ensure that a minimal throughput test can be rapidly executed.",
            NumberOfIterations = 3, RunMode = RunMode.Throughput,
            RunTimeMilliseconds = 1000, TestMode = TestMode.Test)]
        [CounterThroughputAssertion("TestAddTask", MustBe.LessThan, 10000000.0d)]
        [MemoryMeasurement(MemoryMetric.TotalBytesAllocated)]
        [GcTotalAssertion(GcMetric.TotalCollections, GcGeneration.Gen2, MustBe.ExactlyEqualTo, 0.0d)]
        public void BenchmarkAddTask()
        {

        }

        [PerfBenchmark(Description = "Test to ensure that a minimal throughput test can be rapidly executed.",
           NumberOfIterations = 3, RunMode = RunMode.Throughput,
           RunTimeMilliseconds = 1000, TestMode = TestMode.Test)]
        [CounterThroughputAssertion("TestViewTask", MustBe.LessThan, 10000000.0d)]
        [MemoryMeasurement(MemoryMetric.TotalBytesAllocated)]
        [GcTotalAssertion(GcMetric.TotalCollections, GcGeneration.Gen2, MustBe.ExactlyEqualTo, 0.0d)]
        public void BenchmarkViewTask()
        {
            
        }

        [PerfBenchmark(Description = "Test to ensure that a minimal throughput test can be rapidly executed.",
           NumberOfIterations = 3, RunMode = RunMode.Throughput,
           RunTimeMilliseconds = 1000, TestMode = TestMode.Test)]
        [CounterThroughputAssertion("TestUpdateTask", MustBe.LessThan, 10000000.0d)]
        [MemoryMeasurement(MemoryMetric.TotalBytesAllocated)]
        [GcTotalAssertion(GcMetric.TotalCollections, GcGeneration.Gen2, MustBe.ExactlyEqualTo, 0.0d)]
        public void BenchmarkUpdateTask()
        {
            
        }

        [PerfBenchmark(Description = "Test to ensure that a minimal throughput test can be rapidly executed.",
           NumberOfIterations = 3, RunMode = RunMode.Throughput,
           RunTimeMilliseconds = 1000, TestMode = TestMode.Test)]
        [CounterThroughputAssertion("TestEndTask", MustBe.LessThan, 10000000.0d)]
        [MemoryMeasurement(MemoryMetric.TotalBytesAllocated)]
        [GcTotalAssertion(GcMetric.TotalCollections, GcGeneration.Gen2, MustBe.ExactlyEqualTo, 0.0d)]
        public void BenchmarkEndTask()
        {

            
        }

        [PerfCleanup]
        public void Cleanup()
        {
            // does nothing
        }
    }
}
