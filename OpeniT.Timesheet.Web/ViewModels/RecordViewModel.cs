namespace OpeniT.Timesheet.Web.ViewModels
{
	using System;
	using System.Runtime.Serialization;

	using Models;

	public class RecordViewModel
	{
		public int Id { get; set; }

		public string Task { get; set; }

		public string SubTask { get; set; }

		public string Location { get; set; }

		public DateTime Date { get; set; }

		public double Hours { get; set; }

		public double? EpochStart { get; set; }

		public string Description { get; set; }

        public bool IsWFH { get; set; }

		public bool IsLocked { get; set; }

		public int OrderBy { get; set; }

		public string Owner { get; set; }

		[IgnoreDataMember]
		public SubProcess SubProcess { get; set; }

		public int ProcessId => this.SubProcess?.Process.Id ?? 0;

		public string ProcessURIPrefix => this.SubProcess?.Process.TaskUriPrefix ?? string.Empty;

		public int SubProcessId { get; set; }

		public string ProcessName => this.SubProcess?.Process.Name ?? string.Empty;

		public string SubProcessName => this.SubProcess?.Name ?? string.Empty;
	}
}