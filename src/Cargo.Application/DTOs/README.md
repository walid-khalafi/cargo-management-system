# DTO Models Documentation

This directory contains all Data Transfer Object (DTO) models for the cargo management system, organized for clarity and maintainability.

## 📁 **Complete DTO Structure**

### **Common DTOs** (`src/Cargo.Application/DTOs/Common/`)
- **BaseDto.cs** - Foundation class with common properties (Id, CreatedAt, UpdatedAt)
- **AddressDto.cs** - Physical address representation with street, city, province, postal code, country
- **TaxProfileDto.cs** - Tax calculation profile with rates for GST, QST, HST, federal and provincial taxes
- **PlateNumberDto.cs** - Vehicle license plate information with number, province, and country

### **Entity DTOs** - Each in separate files for maintainability

#### **Company DTOs** (`src/Cargo.Application/DTOs/Company/`)
- **CompanyDto.cs** - Complete company entity with name, registration number, address, tax profile, and counts
- **CreateCompanyDto.cs** - For POST endpoints (creating companies) - excludes Id and counts
- **UpdateCompanyDto.cs** - For PUT/PATCH endpoints (updating companies) - includes Id
- **CompanySummaryDto.cs** - Lightweight representation for list views with basic info and counts

#### **Driver DTOs** (`src/Cargo.Application/DTOs/Driver/`)
- **DriverDto.cs** - Complete driver entity with personal info, license details, and company association
- **CreateDriverDto.cs** - For POST endpoints (creating drivers) - excludes Id and computed fields
- **UpdateDriverDto.cs** - For PUT/PATCH endpoints (updating drivers) - includes Id and status

#### **Vehicle DTOs** (`src/Cargo.Application/DTOs/Vehicle/`)
- **VehicleDto.cs** - Complete vehicle entity with specs, registration, status, and ownership
- **CreateVehicleDto.cs** - For POST endpoints (creating vehicles) - excludes Id and status fields
- **UpdateVehicleDto.cs** - For PUT/PATCH endpoints (updating vehicles) - includes all fields
- **VehicleSummaryDto.cs** - Lightweight representation for list views with key info

#### **Route DTOs** (`src/Cargo.Application/DTOs/Route/`)
- **RouteDto.cs** - Complete route entity with origin, destination, waypoints, and assignments
- **CreateRouteDto.cs** - For POST endpoints (creating routes) - excludes Id and actual times
- **UpdateRouteDto.cs** - For PUT/PATCH endpoints (updating routes) - includes all fields
- **RouteSummaryDto.cs** - Lightweight representation for list views
- **WaypointDto.cs** - Route waypoint with location, type, sequence, and timing
- **CreateWaypointDto.cs** - For creating waypoints - excludes Id

#### **DriverBatch DTOs** (`src/Cargo.Application/DTOs/DriverBatch/`)
- **DriverBatchDto.cs** - Complete driver batch entity with entries and calculations
- **CreateDriverBatchDto.cs** - For POST endpoints (creating batches) - excludes Id and totals
- **DriverBatchSummaryDto.cs** - Lightweight representation for list views
- **DriverBatchHourlyDto.cs** - Hourly work entry with date, hours, rate, and total
- **DriverBatchLoadDto.cs** - Load work entry with date, load info, miles, rate, and total
- **DriverBatchWaitDto.cs** - Wait time entry with date, hours, type, rate, and total

## 🎯 **Usage Patterns**

### **API Endpoint Mapping**
| HTTP Method | Endpoint | DTO Class |
|-------------|----------|-----------|
| **POST** | `/api/companies` | `CreateCompanyDto` |
| **PUT** | `/api/companies/{id}` | `UpdateCompanyDto` |
| **GET** | `/api/companies/{id}` | `CompanyDto` |
| **GET** | `/api/companies` | `List<CompanySummaryDto>` |

### **DTO Naming Convention**
- **Create*Dto** - Used for POST requests (creating new entities)
- **Update*Dto** - Used for PUT/PATCH requests (updating existing entities)
- ***Dto** - Used for GET single entity responses
- ***SummaryDto** - Used for GET list responses

## 🏗️ **Architecture Benefits**

### **Separation of Concerns**
- ✅ **Create DTOs** - Only include properties needed for creation
- ✅ **Update DTOs** - Include Id and all updatable properties
- ✅ **Read DTOs** - Include computed properties and relationships
- ✅ **Summary DTOs** - Include only essential properties for lists

### **Consistency Features**
- ✅ **Base Inheritance**: All DTOs inherit from BaseDto
- ✅ **Property Naming**: Consistent naming across all DTOs
- ✅ **Null Handling**: Nullable properties where appropriate
- ✅ **Validation Ready**: Designed for data annotation validation

### **Integration Ready**
- ✅ **AutoMapper Compatible**: Designed for easy mapping
- ✅ **Swagger Compatible**: XML documentation for API documentation
- ✅ **Validation Ready**: Structure supports data annotations
- ✅ **Testable**: Easy to unit test with mock data

## 📊 **Statistics**
- **Total DTO Files**: 19 individual files
- **Common DTOs**: 4 files
- **Entity DTOs**: 15 files across 5 entity types
- **Documentation**: 3 comprehensive documentation files

## 🚀 **Next Steps**
1. Add validation attributes to DTOs
2. Create AutoMapper profiles for entity-to-DTO mapping
3. Add response wrapper DTOs for API responses
4. Create pagination DTOs for list endpoints
5. Add sorting and filtering DTOs

## 📖 **Additional Resources**
- **DTO-Documentation.md** - Detailed property descriptions and examples
- **README-Complete.md** - Comprehensive project documentation
- All DTOs include XML documentation comments for IntelliSense support
