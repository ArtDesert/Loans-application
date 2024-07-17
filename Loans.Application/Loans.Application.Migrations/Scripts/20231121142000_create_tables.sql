create schema if not exists dcs_loans authorization current_user;

create table if not exists dcs_loans.clients
(
	id bigserial primary key,
	last_name varchar(30) not null,
	first_name varchar(30) not null,
	middle_name varchar(30),
	birth_date date not null,
	salary numeric(10, 2)
);

create table if not exists dcs_loans.loans
(
	id bigserial primary key,
	client_id bigint not null,
	amount numeric(10, 2) not null,
	period_in_month integer not null,
	interest_rate numeric(6, 2) not null,
	creation_date date not null,
	loan_status integer not null,
	denial_reason varchar(200),
	foreign key (client_id) references dcs_loans.clients (id) on delete cascade
);

comment on table dcs_loans.clients is 'Таблица клиентов';
comment on table dcs_loans.loans is 'Таблица кредитов';

comment on column dcs_loans.clients.id is 'Идентификатор клиента';
comment on column dcs_loans.clients.last_name is 'Фамилия';
comment on column dcs_loans.clients.first_name is 'Имя';
comment on column dcs_loans.clients.middle_name is 'Отчество';
comment on column dcs_loans.clients.birth_date is 'Дата рождения';
comment on column dcs_loans.clients.salary is 'Зарплата';

comment on column dcs_loans.loans.id is 'Идентификатор кредита';
comment on column dcs_loans.loans.client_id is 'Идентификатор клиента, оформившего кредит';
comment on column dcs_loans.loans.amount is 'Сумма кредита';
comment on column dcs_loans.loans.period_in_month is 'Срок кредита в месяцах';
comment on column dcs_loans.loans.interest_rate is 'Процентная ставка';
comment on column dcs_loans.loans.creation_date is 'Дата создания';
comment on column dcs_loans.loans.loan_status is 'Статус кредита';
comment on column dcs_loans.loans.denial_reason is 'Причина отказа';