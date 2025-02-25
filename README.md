# Inventory
Inventory booking items

# Booking Service API

This API provides a way to fetch booking information from the database. It offers functionality to retrieve all bookings or a specific booking based on the provided `BookingId`.

## Endpoints

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
