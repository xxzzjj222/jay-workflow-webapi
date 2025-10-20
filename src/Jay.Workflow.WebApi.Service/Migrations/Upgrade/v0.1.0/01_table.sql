create table if not exists department
(
	id int auto_increment
		primary key,
	dept_id char(36) charset ascii not null,
	dept_name longtext not null,
	dept_icon longtext null,
	dept_type_id int null,
	dept_type_name longtext null,
	parent_dept_id char(36) charset ascii null,
	status int not null,
	sort_no int not null,
	remark longtext null,
	creator int not null,
	created_time datetime(6) not null,
	modifier int null,
	modified_time datetime(6) null,
	deleter int null,
	deleted_time datetime(6) null
);

create table if not exists system_config
(
	id int auto_increment
		primary key,
	config_key longtext not null,
	config_name longtext not null,
	config_value longtext not null,
	config_type int not null,
	ext_data longtext null,
	sort_no int not null,
	status int not null,
	remark longtext null,
	creator int not null,
	created_time datetime(6) not null,
	modifier int null,
	modified_time datetime(6) null,
	deleter int null,
	deleted_time datetime(6) null
);

