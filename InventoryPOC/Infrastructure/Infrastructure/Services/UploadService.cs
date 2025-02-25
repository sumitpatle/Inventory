using Application.Common.Interface;
using Application.Upload;
using AutoMapper.Execution;
using CsvHelper;
using Member = Domain.Entities.Member;
using Microsoft.Extensions.Logging;
using System.Globalization;
using Azure.Core;
using Domain.Entities;
using CsvHelper.Configuration;

namespace Infrastructure.Services
{
    public class UploadService : IUploadService
    {
        private readonly IBookingDbContext _context;
        private readonly ILogger<UploadService> _logger;

        public UploadService(IBookingDbContext context,
                                      ILogger<UploadService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<string> UploadMembers(UploadCommand request, CancellationToken cancellationToken)
        {
            var Members = new List<Member>();
            int count = 1;

            using (var reader = new StreamReader(request.file.OpenReadStream()))
            {
                var isHeader = true;
                while (!reader.EndOfStream)
                {
                    var line = await reader.ReadLineAsync();
                    if (isHeader)
                    {
                        isHeader = false;
                        continue;
                    }
                    var values = line.Split(',');

                    var member = new Member
                    {
                        Id = count++,
                        Name = Convert.ToString(values[0]),
                        Surname = Convert.ToString(values[1]),
                        BookingCount = int.Parse(values[2]),
                        DateJoined = DateTime.ParseExact(values[3], "yyyy-MM-ddTHH:mm:ss", CultureInfo.InvariantCulture)
,
                        CREATE_USER_ID_CD = "Test_User",
                        CREATE_DATETIME = DateTime.Now
                    };

                    Members.Add(member);
                }
            }

            await _context.MembersTbls.AddRangeAsync(Members);
            await _context.SaveChangesAsync(cancellationToken);

            return "File uploaded and data saved successfully.";

        }

        public async Task<string> UploadInventory(UploadCommand request, CancellationToken cancellationToken)
        {
            var Inventorys = new List<InventoryItem>();
            int count = 1;

            //// Read the CSV file using CsvHelper
            //using (var reader = new StreamReader(csvFilePath))
            //using (var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)
            //{
            //    // Configures CsvHelper to handle commas in quoted fields
            //    ShouldQuote = (field, context) => field.Contains(",")
            //}))
            //{
            //    var records = csv.GetRecords<InventoryItem>();

            //    foreach (var record in records)
            //    {
            //        Console.WriteLine($"Title: {record.Title}");
            //        Console.WriteLine($"Description: {record.Description}");
            //        Console.WriteLine($"Remaining Count: {record.RemainingCount}");
            //        Console.WriteLine($"Expiration Date: {record.ExpirationDate.ToString("dd/MM/yyyy")}");
            //        Console.WriteLine();
            //    }

            //}
            using (var reader = new StreamReader(request.file.OpenReadStream()))
            {
                var isHeader = true;
                while (!reader.EndOfStream)
                {
                    var line = await reader.ReadLineAsync();
                    if (isHeader)
                    {
                        isHeader = false;
                        continue;
                    }
                    var values = line.Split(',');

                    var inventory = new InventoryItem
                    {
                        Id = count++,
                        Title = Convert.ToString(values[0]),
                        Description = Convert.ToString(values[1]),
                        RemainingCount = int.Parse(values[2]),
                        ExpirationDate = DateTime.ParseExact(values[3], "yyyy-MM-ddTHH:mm:ss", CultureInfo.InvariantCulture)
,
                        CREATE_USER_ID_CD = "Test_User",
                        CREATE_DATETIME = DateTime.Now
                    };

                    Inventorys.Add(inventory);
                }
            }

            await _context.InventoryItemsTbls.AddRangeAsync(Inventorys);
            await _context.SaveChangesAsync(cancellationToken);

            return "File uploaded and data saved successfully.";
        }
    }

}
