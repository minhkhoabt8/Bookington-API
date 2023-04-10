CREATE TABLE roles (
        id VARCHAR(40) PRIMARY KEY,
        role_name VARCHAR(50) NOT NULL
);

CREATE TABLE accounts (
    id VARCHAR(40) PRIMARY KEY,
    role_id VARCHAR(40) FOREIGN KEY REFERENCES roles(id) NOT NULL,	
	ref_avatar VARCHAR(256) NOT NULL,
	phone VARCHAR(10) UNIQUE NOT NULL,
	password VARCHAR(100) NOT NULL,
	full_name NVARCHAR(50),
	date_of_birth DATE,
	create_at DATETIME NOT NULL,	
	is_verified BIT NOT NULL,
	is_active BIT NOT NULL,
	is_deleted BIT NOT NULL
);

CREATE TABLE account_otps (
	id VARCHAR(40) PRIMARY KEY,
	ref_account VARCHAR(40) FOREIGN KEY REFERENCES accounts(id) NOT NULL,
	phone VARCHAR(10) NOT NULL,
	otp_code VARCHAR(6) NOT NULL,
	expire_at DATETIME NOT NULL,
	create_at DATETIME NOT NULL,
	is_confirmed BIT NOT NULL
);

CREATE TABLE login_tokens (
	id VARCHAR(40) PRIMARY KEY,
	ref_account VARCHAR(40) FOREIGN KEY REFERENCES accounts(id) NOT NULL,
	token VARCHAR(2048) NOT NULL,
	refresh_token VARCHAR(2048) NOT NULL,	
	create_at DATETIME NOT NULL,
	expire_at DATETIME NOT NULL,
	is_revoked BIT NOT NULL	
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
	name NVARCHAR(250) NOT NULL,
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
	ref_image VARCHAR(40) NOT NULL
);

CREATE TABLE comments (
	id VARCHAR(40) PRIMARY KEY,
	comment_writer_id VARCHAR(40) FOREIGN KEY REFERENCES accounts(id) NOT NULL,
	ref_court VARCHAR(40) FOREIGN KEY REFERENCES courts(id) NOT NULL,
	content NVARCHAR(500) NOT NULL,
	rating DOUBLE PRECISION NOT NULL,
	create_at DATETIME NOT NULL,
	is_active BIT NOT NULL,
	is_deleted BIT NOT NULL
);

CREATE TABLE court_report_responses (
	id VARCHAR(40) PRIMARY KEY,
	content NVARCHAR(1000) NOT NULL
)

CREATE TABLE court_reports (
	id VARCHAR(40) PRIMARY KEY,
	ref_court VARCHAR(40) FOREIGN KEY REFERENCES courts(id) NOT NULL,
	reporter_id VARCHAR(40) FOREIGN KEY REFERENCES accounts(id) NOT NULL,
	ref_response VARCHAR(40) FOREIGN KEY REFERENCES court_report_responses(id),
	content NVARCHAR(1000) NOT NULL,
	is_responded BIT NOT NULL
);

CREATE TABLE user_report_responses (
	id VARCHAR(40) PRIMARY KEY,
	content NVARCHAR(1000) NOT NULL
)

CREATE TABLE user_reports (
	id VARCHAR(40) PRIMARY KEY,
	ref_user VARCHAR(40) FOREIGN KEY REFERENCES accounts(id) NOT NULL,
	reporter_id VARCHAR(40) FOREIGN KEY REFERENCES accounts(id) NOT NULL,
	ref_response VARCHAR(40) FOREIGN KEY REFERENCES user_report_responses(id),
	content NVARCHAR(1000) NOT NULL,
	is_responded BIT NOT NULL
);

CREATE TABLE court_types (
	id VARCHAR(40) PRIMARY KEY,
	content NVARCHAR(50) NOT NULL
);

CREATE TABLE sub_courts (
	id VARCHAR(40) PRIMARY KEY,
	parent_court_id VARCHAR(40) FOREIGN KEY REFERENCES courts(id) NOT NULL,
	court_type_id VARCHAR(40) FOREIGN KEY REFERENCES court_types(id) NOT NULL,
	name NVARCHAR(100) NOT NULL,
	create_at DATETIME NOT NULL,
	is_active BIT NOT NULL,
	is_deleted BIT NOT NULL
);

CREATE TABLE slots (
	id VARCHAR(40) PRIMARY KEY,
	start_time TIME NOT NULL,
	end_time TIME NOT NULL,
	days_in_schedule VARCHAR(20) NOT NULL,
);

CREATE TABLE sub_court_slots (
	id VARCHAR(40) PRIMARY KEY,
	ref_sub_court VARCHAR(40) FOREIGN KEY REFERENCES sub_courts(id) NOT NULL,
	ref_slot VARCHAR(40) FOREIGN KEY REFERENCES slots(id) NOT NULL,
	price DOUBLE PRECISION NOT NULL,
	is_active BIT NOT NULL
)

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
	is_active BIT NOT NULL,
	is_deleted BIT NOT NULL
);

CREATE TABLE momo_transactions (
	id VARCHAR(40) PRIMARY KEY,
	content NVARCHAR(1000) NOT NULL,
	amount DOUBLE PRECISION NOT NULL,
	create_at DATETIME NOT NULL,
	is_successful BIT NOT NULL
);

CREATE TABLE transactions (
	id VARCHAR(40) PRIMARY KEY,
	ref_from VARCHAR(40) FOREIGN KEY REFERENCES accounts(id) NOT NULL,
	ref_to VARCHAR(40) FOREIGN KEY REFERENCES accounts(id) NOT NULL,
	ref_momo_transaction VARCHAR(40) FOREIGN KEY REFERENCES momo_transactions(id),
	amount DOUBLE PRECISION NOT NULL,
	reason NVARCHAR(500) NOT NULL,
	create_at DATETIME NOT NULL	
);

CREATE TABLE orders (
	id VARCHAR(40) PRIMARY KEY,
	create_by VARCHAR(40) FOREIGN KEY REFERENCES accounts(id),
	transaction_id VARCHAR(40) FOREIGN KEY REFERENCES transactions(id),
	voucher_code VARCHAR(10) FOREIGN KEY REFERENCES vouchers(voucher_code),
	order_at DATETIME NOT NULL,
	original_price DOUBLE PRECISION NOT NULL,	
	total_price DOUBLE PRECISION NOT NULL,
	is_paid BIT NOT NULL,
	is_canceled BIT NOT NULL,
	is_refunded BIT NOT NULL
);

CREATE TABLE bookings (
	id VARCHAR(40) PRIMARY KEY,
	ref_slot VARCHAR(40) FOREIGN KEY REFERENCES slots(id) NOT NULL,
	ref_sub_court VARCHAR(40) FOREIGN KEY REFERENCES sub_courts(id) NOT NULL,
	ref_order VARCHAR(40) FOREIGN KEY REFERENCES orders(id) NOT NULL,
	book_by VARCHAR(40) FOREIGN KEY REFERENCES accounts(id) NOT NULL,
	book_at DATETIME NOT NULL,
	play_date DATE NOT NULL,
	price DOUBLE PRECISION NOT NULL,
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

CREATE TABLE notifications (
	id VARCHAR(40) PRIMARY KEY,
	ref_account VARCHAR(40) FOREIGN KEY REFERENCES accounts(id) NOT NULL,
	content VARCHAR(500) NOT NULL,
	create_at DATETIME NOT NULL,
	is_read BIT NOT NULL
);

CREATE TABLE ads (
	id VARCHAR(40) PRIMARY KEY,
	title NVARCHAR(200) NOT NULL,
	ref_court VARCHAR(40) FOREIGN KEY REFERENCES courts(id),
	ref_image VARCHAR(40) NOT NULL,
	promotion_order INT NOT NULL,
	start_time DATETIME NOT NULL,
	end_time DATETIME NOT NULL,
	ad_link VARCHAR(2048) NOT NULL,
	is_court_ad BIT NOT NULL,
	is_deleted BIT NOT NULL
);

CREATE TABLE bans (
	id VARCHAR(40) PRIMARY KEY,
	ref_account VARCHAR(40) FOREIGN KEY REFERENCES accounts(id),
	ref_court VARCHAR(40) FOREIGN KEY REFERENCES courts(id),
	reason NVARCHAR(500) NOT NULL,
	duration INT NOT NULL,	
	ban_until DATETIME,
	create_at DATETIME NOT NULL,
	is_account_ban BIT NOT NULL,
	is_court_ban BIT NOT NULL,
	is_active BIT NOT NULL
);

CREATE TABLE matches (
	id VARCHAR(40) PRIMARY KEY,
	host_by VARCHAR(40) FOREIGN KEY REFERENCES accounts(id) NOT NULL,
	ref_booking VARCHAR(40) FOREIGN KEY REFERENCES bookings(id) NOT NULL,
	num_of_players_allowed INT NOT NULL,
	num_of_rounds INT NOT NULL,
	match_code VARCHAR(6),
	is_payment_splitted BIT NOT NULL
);

CREATE TABLE competitions (
	id VARCHAR(40) PRIMARY KEY,
	host_by VARCHAR(40) FOREIGN KEY REFERENCES accounts(id) NOT NULL,
	name NVARCHAR(500) NOT NULL,
	description NVARCHAR(2500) NOT NULL,
	num_of_teams_allowed INT NOT NULL,
	competition_code VARCHAR(6) NOT NULL,
	start_date DATETIME NOT NULL,
	end_date DATETIME NOT NULL,
	register_deadline DATETIME NOT NULL,
	is_started BIT NOT NULL
);

CREATE TABLE competition_matches (
	id VARCHAR(40) PRIMARY KEY,
	ref_competition VARCHAR(40) FOREIGN KEY REFERENCES competitions(id) NOT NULL,
	ref_match VARCHAR(40) FOREIGN KEY REFERENCES matches(id) NOT NULL,
	match_position INT NOT NULL
);

CREATE TABLE teams (
	id VARCHAR(40) PRIMARY KEY,
	ref_match VARCHAR(40) FOREIGN KEY REFERENCES matches(id) NOT NULL,
	ref_competition VARCHAR(40) FOREIGN KEY REFERENCES competitions(id),
	name NVARCHAR(50) NOT NULL,
	team_code VARCHAR(6),
	is_competition_team BIT NOT NULL
)

CREATE TABLE match_teams (
	id VARCHAR(40) PRIMARY KEY,
	ref_match VARCHAR(40) FOREIGN KEY REFERENCES matches(id) NOT NULL,
	ref_team VARCHAR(40) FOREIGN KEY REFERENCES teams(id) NOT NULL
);

CREATE TABLE rounds (
	id VARCHAR(40) PRIMARY KEY,
	ref_match VARCHAR(40) FOREIGN KEY REFERENCES matches(id) NOT NULL,
	ref_team VARCHAR(40) FOREIGN KEY REFERENCES teams(id) NOT NULL,
	round_num INT NOT NULL,
	point INT NOT NULL
);

CREATE TABLE team_players (
	id VARCHAR(40) PRIMARY KEY,
	ref_team VARCHAR(40) FOREIGN KEY REFERENCES teams(id) NOT NULL,
	ref_account VARCHAR(40) FOREIGN KEY REFERENCES accounts(id) NOT NULL
);