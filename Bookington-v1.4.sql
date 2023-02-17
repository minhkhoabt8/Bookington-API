

USE master
GO

-- Drop the database if it already exists
IF  EXISTS (
	SELECT name 
		FROM sys.databases 
		WHERE name = N'BookingtonDB'
)
DROP DATABASE BookingtonDB
GO

CREATE DATABASE BookingtonDB
GO

USE BookingtonDB

CREATE TABLE roles (
        id VARCHAR(40) PRIMARY KEY,
        role_name VARCHAR(50) NOT NULL
);

CREATE TABLE accounts (
        id VARCHAR(40) PRIMARY KEY,
        role_id VARCHAR(40) FOREIGN KEY REFERENCES roles(id) NOT NULL,
	phone VARCHAR(10) UNIQUE NOT NULL,
	password VARCHAR(100) NOT NULL,
	full_name NVARCHAR(50),
	date_of_birth DATE,
	create_at DATETIME NOT NULL,	
	is_active BIT NOT NULL
);

CREATE TABLE account_otps (
	id VARCHAR(40) PRIMARY KEY,
	phone VARCHAR(10) FOREIGN KEY REFERENCES accounts(phone) NOT NULL,
	otp_code VARCHAR(6) NOT NULL,
	expire_at DATETIME NOT NULL,
	create_at DATETIME NOT NULL,
	is_confirmed BIT NOT NULL
);

CREATE TABLE provinces (
	id VARCHAR(40) PRIMARY KEY,
	province_name NVARCHAR(50) NOT NULL
);

CREATE TABLE districts (
	id VARCHAR(40) PRIMARY KEY,
	province_id VARCHAR(40) FOREIGN KEY REFERENCES provinces(id) NOT NULL,
	district_name NVARCHAR(50) NOT NULL
);

CREATE TABLE courts (
	id VARCHAR(40) PRIMARY KEY,
	owner_id VARCHAR(40) FOREIGN KEY REFERENCES accounts(id) NOT NULL,
	district_id VARCHAR(40) FOREIGN KEY REFERENCES districts(id) NOT NULL,
	name NVARCHAR(100) NOT NULL,
	address NVARCHAR(200),
	description NVARCHAR(1000),
	open_at TIME NOT NULL,
	close_at TIME NOT NULL,
	create_at DATETIME NOT NULL,
	is_active BIT NOT NULL,
	is_deleted BIT NOT NULL
);

CREATE TABLE court_images (
	id VARCHAR(40) PRIMARY KEY,
	court_id VARCHAR(40) FOREIGN KEY REFERENCES courts(id) NOT NULL,
	image_binary VARBINARY(MAX)
);

CREATE TABLE comments (
	id VARCHAR(40) PRIMARY KEY,
	comment_writer_id VARCHAR(40) FOREIGN KEY REFERENCES accounts(id) NOT NULL,
	ref_court VARCHAR(40) FOREIGN KEY REFERENCES courts(id) NOT NULL,
	content NVARCHAR(500) NOT NULL,
	rating DOUBLE PRECISION NOT NULL,
	create_at DATETIME NOT NULL,
	is_active BIT NOT NULL	
);

CREATE TABLE court_reports (
	id VARCHAR(40) PRIMARY KEY,
	ref_court VARCHAR(40) FOREIGN KEY REFERENCES courts(id) NOT NULL,
	reporter_id VARCHAR(40) FOREIGN KEY REFERENCES accounts(id) NOT NULL,
	content NVARCHAR(500) NOT NULL
);

CREATE TABLE user_reports (
	id VARCHAR(40) PRIMARY KEY,
	ref_user VARCHAR(40) FOREIGN KEY REFERENCES accounts(id) NOT NULL,
	reporter_id VARCHAR(40) FOREIGN KEY REFERENCES accounts(id) NOT NULL,
	content NVARCHAR(500) NOT NULL
);

CREATE TABLE court_types (
	id VARCHAR(40) PRIMARY KEY,
	content NVARCHAR(50) NOT NULL
);

CREATE TABLE sub_courts (
	id VARCHAR(40) PRIMARY KEY,
	name NVARCHAR(100) NOT NULL,
	parent_court_id VARCHAR(40) FOREIGN KEY REFERENCES courts(id) NOT NULL,
	court_type_id VARCHAR(40) FOREIGN KEY REFERENCES court_types(id) NOT NULL,
	create_at DATETIME NOT NULL,
	is_active BIT NOT NULL,
	is_deleted BIT NOT NULL
);

CREATE TABLE slots (
	id VARCHAR(40) PRIMARY KEY,
	ref_sub_court VARCHAR(40) FOREIGN KEY REFERENCES sub_courts(id) NOT NULL,
	start_time TIME NOT NULL,
	end_time TIME NOT NULL,
	price DOUBLE PRECISION NOT NULL,
	is_active BIT NOT NULL
);

CREATE TABLE vouchers (
	id VARCHAR(40) PRIMARY KEY,
	create_by VARCHAR(40) FOREIGN KEY REFERENCES accounts(id) NOT NULL,
	ref_court VARCHAR(40) FOREIGN KEY REFERENCES courts(id) NOT NULL,
	voucher_code VARCHAR(10) UNIQUE NOT NULL,
	title NVARCHAR(50) NOT NULL,
	description NVARCHAR(250),
	discount DOUBLE PRECISION NOT NULL,
	usages INTEGER NOT NULL,
	max_quantity INTEGER NOT NULL,
	start_date DATETIME,
	end_date DATETIME,
	create_at DATETIME NOT NULL,
	is_active BIT NOT NULL
);

CREATE TABLE orders (
	id VARCHAR(40) PRIMARY KEY,
	transaction_id VARCHAR(100),
	order_at DATETIME NOT NULL,
	total_price DOUBLE PRECISION NOT NULL,
	is_paid BIT NOT NULL,
	is_canceled BIT NOT NULL,
	is_refunded BIT NOT NULL
);

CREATE TABLE bookings (
	id VARCHAR(40) PRIMARY KEY,
	ref_slot VARCHAR(40) FOREIGN KEY REFERENCES slots(id) NOT NULL,
	ref_order VARCHAR(40) FOREIGN KEY REFERENCES orders(id) NOT NULL,
	book_by VARCHAR(40) FOREIGN KEY REFERENCES accounts(id) NOT NULL,
	voucher_code VARCHAR(10) FOREIGN KEY REFERENCES vouchers(voucher_code) NOT NULL,
	book_at DATETIME NOT NULL,
	play_date DATE NOT NULL,
	price DOUBLE PRECISION NOT NULL,
	original_price DOUBLE PRECISION NOT NULL	
);

CREATE TABLE chat_rooms (
	id VARCHAR(40) PRIMARY KEY,
	ref_owner VARCHAR(40) FOREIGN KEY REFERENCES accounts(id) NOT NULL,
	ref_user VARCHAR(40) FOREIGN KEY REFERENCES accounts(id) NOT NULL,	
	is_active BIT NOT NULL
);

CREATE TABLE chat_messages (
	id VARCHAR(40) PRIMARY KEY,
	ref_chatroom VARCHAR(40) FOREIGN KEY REFERENCES chat_rooms(id) NOT NULL,
	ref_owner VARCHAR(40) FOREIGN KEY REFERENCES accounts(id) NOT NULL,
	ref_user VARCHAR(40) FOREIGN KEY REFERENCES accounts(id) NOT NULL,
	create_at DATETIME NOT NULL,
	sequence_order INTEGER NOT NULL,		
	is_deleted BIT NOT NULL
);

CREATE TABLE user_balances (
	id VARCHAR(40) PRIMARY KEY,
	ref_user VARCHAR(40) FOREIGN KEY REFERENCES accounts(id) NOT NULL,
	balance DOUBLE PRECISION NOT NULL
);

CREATE TABLE transaction_history (
	id VARCHAR(40) PRIMARY KEY,
	ref_from VARCHAR(40) FOREIGN KEY REFERENCES accounts(id) NOT NULL,
	ref_to VARCHAR(40) FOREIGN KEY REFERENCES accounts(id) NOT NULL,
	amount DOUBLE PRECISION NOT NULL,
	create_at DATETIME NOT NULL	
);