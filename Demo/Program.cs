// See https://aka.ms/new-console-template for more information



var recordGenerator = new Model.SampleRecordGenerator();
var records = recordGenerator.CreateRecords(30000);

Console.Clear();

Utils.RecordDisplay.PresentRulesResults(records.ToArray());