using Microsoft.Extensions.Logging;

namespace RuoYi.Common.Job
{
    public class JobService
    {
        private readonly ILogger<JobService> _logger;

        public JobService(ILogger<JobService> logger)
        {
            _logger = logger;
        }

        public string EnqueueJob(string jobName, Func<Task> jobFunc)
        {
            var jobId = Hangfire.BackgroundJob.Enqueue(() => jobFunc());
            _logger.LogInformation("任务 {JobName} 已加入队列, JobId: {JobId}", jobName, jobId);
            return jobId;
        }

        public string ScheduleJob(string jobName, Func<Task> jobFunc, TimeSpan delay)
        {
            var jobId = Hangfire.BackgroundJob.Schedule(() => jobFunc(), delay);
            _logger.LogInformation("任务 {JobName} 已调度, JobId: {JobId}, 延迟: {Delay}", jobName, jobId, delay);
            return jobId;
        }

        public string AddRecurringJob(string jobName, Func<Task> jobFunc, string cronExpression)
        {
            Hangfire.RecurringJob.AddOrUpdate(jobName, () => jobFunc(), cronExpression);
            _logger.LogInformation("定时任务 {JobName} 已添加, Cron: {Cron}", jobName, cronExpression);
            return jobName;
        }

        public bool DeleteJob(string jobId)
        {
            var result = Hangfire.BackgroundJob.Delete(jobId);
            _logger.LogInformation("任务 {JobId} 删除结果: {Result}", jobId, result);
            return result;
        }

        public bool DeleteRecurringJob(string jobName)
        {
            Hangfire.RecurringJob.RemoveIfExists(jobName);
            _logger.LogInformation("定时任务 {JobName} 已删除", jobName);
            return true;
        }
    }
}