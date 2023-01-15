CREATE TABLE roles (
    id VARCHAR(40) PRIMARY KEY,
    role_name VARCHAR(10)
);

CREATE TABLE accounts (
    id VARCHAR(40) PRIMARY KEY,
    role_id VARCHAR(40) FOREIGN KEY REFERENCES roles(id),
	phone VARCHAR(10) UNIQUE,
	password VARCHAR(100),
	full_name NVARCHAR(50),
	create_at DATETIME,	
	is_active BIT
);

CREATE TABLE account_otps (
	id VARCHAR(40) PRIMARY KEY,
	phone VARCHAR(10) FOREIGN KEY REFERENCES accounts(phone),
	otp VARCHAR(6),
	create_at DATETIME,
	is_confirmed BIT
);

CREATE TABLE provinces (
	id VARCHAR(40) PRIMARY KEY,
	province_name NVARCHAR(50)
);

CREATE TABLE districts (
	id VARCHAR(40) PRIMARY KEY,
	province_id VARCHAR(40) FOREIGN KEY REFERENCES provinces(id),
	district_name NVARCHAR(50)
);

CREATE TABLE courts (
	id VARCHAR(40) PRIMARY KEY,
	owner_id VARCHAR(40) FOREIGN KEY REFERENCES accounts(id),
	district_id VARCHAR(40) FOREIGN KEY REFERENCES districts(id),
	name NVARCHAR(100),
	address NVARCHAR(200),
	open_at TIME,
	close_at TIME,
	create_at DATETIME,
	is_active BIT,
	is_deleted BIT
);

CREATE TABLE court_images (
	id VARCHAR(40) PRIMARY KEY,
	court_id VARCHAR(40) FOREIGN KEY REFERENCES courts(id),
	image_binary VARBINARY(MAX)
);

CREATE TABLE comments (
	id VARCHAR(40) PRIMARY KEY,
	comment_writer_id VARCHAR(40) FOREIGN KEY REFERENCES accounts(id),
	ref_court VARCHAR(40) FOREIGN KEY REFERENCES courts(id),
	content NVARCHAR(500),
	rating DOUBLE PRECISION,
	create_at DATETIME,
	is_active BIT	
);

CREATE TABLE report_types (
	id VARCHAR(40) PRIMARY KEY,
	report_name NVARCHAR(50)
);

CREATE TABLE reports (
	id VARCHAR(40) PRIMARY KEY,
	type_id VARCHAR(40) FOREIGN KEY REFERENCES report_types(id),
	reporter_id VARCHAR(40) FOREIGN KEY REFERENCES accounts(id),
	content NVARCHAR(500)
);

CREATE TABLE court_types (
	id VARCHAR(40) PRIMARY KEY,
	content NVARCHAR(50)
);

CREATE TABLE sub_courts (
	id VARCHAR(40) PRIMARY KEY,
	name NVARCHAR(100),
	parent_court_id VARCHAR(40) FOREIGN KEY REFERENCES courts(id),
	court_type_id VARCHAR(40) FOREIGN KEY REFERENCES court_types(id),
	create_at DATETIME,
	is_active BIT,
	is_deleted BIT
);

CREATE TABLE slots (
	id VARCHAR(40) PRIMARY KEY,
	ref_sub_court VARCHAR(40) FOREIGN KEY REFERENCES sub_courts(id),
	start_time TIME,
	end_time TIME,
	price DOUBLE PRECISION,
	is_active BIT
);

CREATE TABLE vouchers (
	id VARCHAR(40) PRIMARY KEY,
	create_by VARCHAR(40) FOREIGN KEY REFERENCES accounts(id),
	ref_court VARCHAR(40) FOREIGN KEY REFERENCES courts(id),
	voucher_code VARCHAR(10) UNIQUE,
	title NVARCHAR(50),
	description NVARCHAR(250),
	discount DOUBLE PRECISION,
	usages INTEGER,
	max_quantity INTEGER,
	start_date DATETIME,
	end_date DATETIME,
	create_at DATETIME,
	is_active BIT
);

CREATE TABLE bookings (
	id VARCHAR(40) PRIMARY KEY,
	ref_slot VARCHAR(40) FOREIGN KEY REFERENCES slots(id),
	book_by VARCHAR(40) FOREIGN KEY REFERENCES accounts(id),
	voucher_code VARCHAR(10) FOREIGN KEY REFERENCES vouchers(voucher_code),
	book_at DATETIME,
	play_date DATE,
	price DOUBLE PRECISION,
	original_price DOUBLE PRECISION,
	is_paid BIT,
	is_canceled BIT,
	is_refunded BIT
);

CREATE TABLE orders (
	id VARCHAR(40) PRIMARY KEY,
	booking_ref VARCHAR(40) FOREIGN KEY REFERENCES bookings(id),
	order_at DATETIME,
	price DOUBLE PRECISION
);