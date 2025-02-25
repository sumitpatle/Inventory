# Inventory
Inventory booking items

# Booking Service API

This API provides a way to fetch booking information from the database. It offers functionality to retrieve all bookings or a specific booking based on the provided `BookingId`.

## Technologies Used

- **ASP.NET Core**: Framework for building APIs.
- **Entity Framework Core**: ORM used for interacting with the database.
- **SQLlite**: Database for storing booking data.
- **AutoMapper** : Mapping
- **csvhelper** : parsing csv file.
- **MediatR** : Query and Command Pattern.

## How to Run

1. Clone the repository.
2. Run the following commands to restore dependencies and run the project:
   ```bash
   dotnet restore
   dotnet build
   dotnet run

## How to Start
1. Upload file member and inventory from 

## Endpoints

### `POST api/upload`

- **Description**: Upload Member and Inventory csv file and save Data in Database.
- **Query Parameters**: File and FileType(Member or Inventory)
- **Response**: Success or Failure.

### `POST api/booking/book`

- **Description**: Book the Item.
- **Query Parameters**: memberId and inventoryItemId
- **Response**: Success or Failure.
- **Example Request Payload**:
  ```json
  {
  "memberId": 0,
  "inventoryItemId": 0
  }

### `POST api/booking/cancel`

- **Description**: Cancel booking.
- **Query Parameters**: bookingId.
- **Response**: Success or Failure.
  - **Example Request Payload**:
  ```json
  {
  "bookingId": 0
  }
  
### `GET /bookings`

- **Description**: Retrieves a list of all bookings.
- **Query Parameters**: None
- **Response**: Returns a list of bookings.
- **Example Response**:
  ```json
  [
    {
      "Id": 1,
      "MemberId": 101,
      "InventoryItemId": 202,
      "BookingDate": "2025-02-25T14:30:00"
    },
    {
      "Id": 2,
      "MemberId": 102,
      "InventoryItemId": 203,
      "BookingDate": "2025-02-26T10:00:00"
    }
  ]

### `POST api/Data/member`

- **Description**: Getting all members.
- **Query Parameters**: None.
- **Response**: Success or Failure.
  - **Example Response Payload**:
  ```json
  [
    {
      "id": 1,
      "name": "Sophie",
      "surname": "Davis",
      "bookingCount": 2,
      "dateJoined": "2024-01-02T12:10:11"
    },
    {
      "id": 2,
      "name": "Emily",
      "surname": "Johnson",
      "bookingCount": 0,
      "dateJoined": "2024-11-12T12:10:12"
    }
  ]

### `POST api/Data/inventory`

- **Description**: Getting all inventory.
- **Query Parameters**: None.
- **Response**: Success or Failure.
  - **Example Response Payload**:
  ```json
  [
    {
      "id": 1,
      "title": "Bali",
      "description": "Suspendisse",
      "remainingCount": 4,
      "expirationDate": "2030-11-19T00:00:00"
    },
    {
      "id": 2,
      "title": "Madeira",
      "description": "Donec.",
      "remainingCount": 4,
      "expirationDate": "2030-11-20T00:00:00"
    },
  ]
  
