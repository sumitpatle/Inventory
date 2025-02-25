using Application.Common.Interface;
using Application.Upload;
using CsvHelper;
using CsvHelper.Configuration;
using Domain.Entities;
using Microsoft.Extensions.Logging;
using System.Globalization;
using Member = Domain.Entities.Member;

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
                        LAST_UPDATE_USER_ID_CD = "Test_User",
                        CREATE_DATETIME = DateTime.Now,
                        LAST_UPDATE_DATETIME = DateTime.Now,
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
            try
            {
                string _storagePath = Path.Combine(Directory.GetCurrentDirectory(), "UploadedFiles");

                if (!Directory.Exists(_storagePath))
                {
                    Directory.CreateDirectory(_storagePath);
                }
                var filePath = Path.Combine(_storagePath, Guid.NewGuid().ToString() + Path.GetExtension(request.file.FileName));

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await request.file.CopyToAsync(stream);
                }

                var Inventorys = new List<InventoryItem>();
                int count = 1;

                using (var reader = new StreamReader(filePath))
                using (var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)
                {
                    ShouldQuote = field => field.Field.Contains(",")
                }))
                {
                    var records = csv.GetRecords<InventoryItemDto>();

                    foreach (var record in records)
                    {
                        Console.WriteLine($"Title: {record.title}");
                        Console.WriteLine($"Description: {record.description}");
                        Console.WriteLine($"Remaining Count: {record.remaining_count}");
                        Console.WriteLine($"Expiration Date: {record.expiration_date}");
                        Console.WriteLine();

                        var inventory = new InventoryItem
                        {
                            Id = count++,
                            Title = Convert.ToString(record.title),
                            Description = Convert.ToString(record.description),
                            RemainingCount = record.remaining_count,
                            ExpirationDate = DateTime.ParseExact(record.expiration_date, "dd-MM-yyyy", CultureInfo.InvariantCulture)
    ,
                            CREATE_USER_ID_CD = "Test_User",
                            LAST_UPDATE_USER_ID_CD = "Test_User",
                            CREATE_DATETIME = DateTime.Now,
                            LAST_UPDATE_DATETIME = DateTime.Now,
                        };

                        Inventorys.Add(inventory);
                    }
                }
                await _context.InventoryItemsTbls.AddRangeAsync(Inventorys);
                await _context.SaveChangesAsync(cancellationToken);

                return "File uploaded and data saved successfully.";
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
