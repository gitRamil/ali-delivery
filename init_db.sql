#Создание Users.

WITH
passport1 AS (
    INSERT INTO passport (
        id, passport_number, registration_date,
        type_id, created_by,
        created_date, updated_by, updated_date, issued_by 
    )
    VALUES (
        gen_random_uuid(), '77777777777', '2020-04-12', '3a15d9e1-c9a0-80ab-eac9-9369b2ace783',
        'admin',
        NOW(), 'admin',
        NOW(),'MVD RF'
    )
    RETURNING id
),
passport2 AS (
    INSERT INTO passport (
        id, passport_number, registration_date,
        type_id, created_by,
        created_date, updated_by, updated_date, issued_by 
    )
    VALUES (
        gen_random_uuid(), '999999999', '2021-04-12', '3a15d9e1-c9a1-8b19-64f4-8cb3007b8a5d',
        'admin',
        NOW(), 'admin',
        NOW(),'MVD RK'
    )
    RETURNING id
),
passport3 AS (
    INSERT INTO passport (
        id, passport_number, registration_date,
        type_id, created_by,
        created_date, updated_by, updated_date, issued_by 
    )
    VALUES (
        gen_random_uuid(), '8888888888', '2018-04-12', '3a15d9e1-c9a0-80ab-eac9-9369b2ace783',
        'admin',
        NOW(), 'admin',
        NOW(),'MVD RF'
    )
    RETURNING id
),
passport4 AS (
    INSERT INTO passport (
        id, passport_number, registration_date,
        type_id, created_by,
        created_date, updated_by, updated_date, issued_by 
    )
    VALUES (
        gen_random_uuid(), '444444444', '2015-04-12', '3a15d9e1-c9a0-80ab-eac9-9369b2ace783',
        'admin',
        NOW(), 'admin',
        NOW(),'MVD RF'
    )
    RETURNING id
),
passport5 AS (
    INSERT INTO passport (
        id, passport_number, registration_date,
        type_id, created_by,
        created_date, updated_by, updated_date, issued_by 
    )
    VALUES (
        gen_random_uuid(), '8888888888', '2016-04-12', '3a15d9e1-c9a1-8b19-64f4-8cb3007b8a5d',
        'admin',
        NOW(), 'admin', NOW(), 'MVD RK'
)
returning id
),
passport6 AS (
    INSERT INTO passport (
        id, passport_number, registration_date,
        type_id, created_by,
        created_date, updated_by, updated_date, issued_by 
    )
    VALUES (
        gen_random_uuid(), '8888888888', '2012-04-12', '3a15d9e1-c9a1-8b19-64f4-8cb3007b8a5d',
        'admin',
        NOW(), 'admin',
        NOW(),'MVD RK'
    )
    RETURNING id
)
INSERT INTO users (
    id, login, passport_info_id, password, role_id, birth_day, first_name, last_name, created_by, created_date, updated_by, updated_date
)
values 
    (gen_random_uuid(), 'Basic', (SELECT id FROM passport1), '$2a$11$Ks7AEOVVMDqsIjzpFEaHb.c9NfzeqBk/QhPnuiqbERfE186QBjS4a','3a1844b2-1e4f-f70c-c599-5fe824c7a873', '1990-01-01', 'Петр', 'Высший', 'Admin', NOW(), 'Admin' ,NOW()),
    (gen_random_uuid(), 'Basic1', (SELECT id FROM passport4), '$2a$11$Ks7AEOVVMDqsIjzpFEaHb.c9NfzeqBk/QhPnuiqbERfE186QBjS4a','3a1844b2-1e4f-f70c-c599-5fe824c7a873', '1985-05-05', 'Вадим', 'Терещенко', 'Admin', NOW(), 'Admin' ,NOW()),
    (gen_random_uuid(), 'Basic2', (SELECT id FROM passport3), '$2a$11$Ks7AEOVVMDqsIjzpFEaHb.c9NfzeqBk/QhPnuiqbERfE186QBjS4a','3a1844b2-1e4f-f70c-c599-5fe824c7a873', '2000-12-12', 'Александр', 'Герасимов', 'Admin', NOW(), 'Admin' ,NOW()),
   (gen_random_uuid(), 'Cour', (SELECT id FROM passport2), '$2a$11$Ks7AEOVVMDqsIjzpFEaHb.c9NfzeqBk/QhPnuiqbERfE186QBjS4a','3a1844b2-1e60-6e25-1bcf-da74b2a4f07c', '1990-01-01', 'Иван', 'Логов', 'Admin', NOW(), 'Admin' ,NOW()),
    (gen_random_uuid(), 'Cour1', (SELECT id FROM passport5), '$2a$11$Ks7AEOVVMDqsIjzpFEaHb.c9NfzeqBk/QhPnuiqbERfE186QBjS4a','3a1844b2-1e60-6e25-1bcf-da74b2a4f07c', '1985-05-05', 'Михаил', 'Осетин', 'Admin', NOW(), 'Admin' ,NOW()),
    (gen_random_uuid(), 'Cour2', (SELECT id FROM passport6), '$2a$11$Ks7AEOVVMDqsIjzpFEaHb.c9NfzeqBk/QhPnuiqbERfE186QBjS4a','3a1844b2-1e60-6e25-1bcf-da74b2a4f07c', '2000-12-12', 'Даниил', 'Герц', 'Admin', NOW(), 'Admin' ,NOW())

#Создание NotAuthUsers.

INSERT INTO not_auth_users (id, first_name, last_name, phone_number, creator_id)
SELECT gen_random_uuid(), 'Олег', 'Петров', '+7778885566', id FROM users WHERE login = 'Basic'
UNION ALL
SELECT gen_random_uuid(), 'Фарух', 'Муродов', '+7777775566', id FROM users WHERE login = 'Basic1'
UNION ALL
SELECT gen_random_uuid(), 'Алексей', 'Иванов', '+7779995566', id FROM users WHERE login = 'Basic2';

#Создание заказов.

WITH 
order_info1 AS (
    INSERT INTO order_details(
        id,address_from, address_to,
        price, weight, size_id,
        created_by, created_date, updated_by, updated_date
    )
    VALUES (
        gen_random_uuid(), 'Address A1', 'Address B1', 150.00, 200.0,
        '3a156e1f-6055-abfb-36b7-7e630cc807b9', 'admin', NOW(),
        'admin', NOW()
    )
    RETURNING id
),
order_info2 AS (
    INSERT INTO order_details(
        id,address_from, address_to,
        price, weight, size_id,
        created_by, created_date, updated_by, updated_date
    )
    VALUES (
        gen_random_uuid(), 'Address A2', 'Address B2', 2000.00, 500.0,
        '3a156e1f-6056-875d-e42d-3e8e7ec6e082', 'admin', NOW(),
        'admin', NOW()
    )
    RETURNING id
),
order_info3 AS (
    INSERT INTO order_details(
        id,address_from, address_to,
        price, weight, size_id,
        created_by, created_date, updated_by, updated_date
    )
    VALUES (
        gen_random_uuid(), 'Address A3', 'Address B3', 300.00, 300.0,
        '3a156e1f-6055-abfb-36b7-7e630cc807b9', 'admin', NOW(),
        'admin', NOW()
    )
    RETURNING id
),
order_info4 AS (
    INSERT INTO order_details(
        id,address_from, address_to,
        price, weight, size_id,
        created_by, created_date, updated_by, updated_date
    )
    VALUES (
        gen_random_uuid(), 'Address A4', 'Address B4', 1500.00, 450.0,
        '3a156e1f-6056-875d-e42d-3e8e7ec6e082', 'admin', NOW(),
        'admin', NOW()
    )
    RETURNING id
),
order_info5 AS (
    INSERT INTO order_details(
        id,address_from, address_to,
        price, weight, size_id,
        created_by, created_date, updated_by, updated_date
    )
    VALUES (
        gen_random_uuid(), 'Address A5', 'Address B5', 10000.00, 1000.0,
        '3a156e1f-6057-39cd-7580-20395231a00f', 'admin', NOW(),
        'admin', NOW()
    )
    RETURNING id
),
order_info6 AS (
    INSERT INTO order_details(
        id,address_from, address_to,
        price, weight, size_id,
        created_by, created_date, updated_by, updated_date
    )
    VALUES (
        gen_random_uuid(), 'Address A6', 'Address B6', 15000.00, 800.0,
        '3a156e1f-6057-39cd-7580-20395231a00f', 'admin', NOW(),
        'admin', NOW()
    )
    RETURNING id
),
order_info7 AS (
    INSERT INTO order_details(
        id,address_from, address_to,
        price, weight, size_id,
        created_by, created_date, updated_by, updated_date
    )
    VALUES (
        gen_random_uuid(), 'Address A7', 'Address B7', 1700.00, 650.0,
        '3a156e1f-6056-875d-e42d-3e8e7ec6e082', 'admin', NOW(),
        'admin', NOW()
    )
    RETURNING id
),
order_info8 AS (
    INSERT INTO order_details(
        id,address_from, address_to,
        price, weight, size_id,
        created_by, created_date, updated_by, updated_date
    )
    VALUES (
        gen_random_uuid(), 'Address A8', 'Address B8', 1550.00, 550.0,
        '3a156e1f-6056-875d-e42d-3e8e7ec6e082', 'admin', NOW(),
        'admin', NOW()
    )
    RETURNING id
)
INSERT INTO orders (
    id, created_by, created_date, name, updated_by, updated_date, 
    courier_id, details_id, not_auth_receiver_id, receiver_id, sender_id, status_id
)
VALUES
    (gen_random_uuid(), 'admin', NOW(), 'Заказ 1', 'admin', NOW(), null, 
     (SELECT id FROM order_info1), NULL, (SELECT id FROM users WHERE login = 'Basic'), 
     (SELECT id FROM users WHERE login = 'Basic1'), '3a174d9d-c0e0-b9f8-286f-aa381bbf2d0a'),
(gen_random_uuid(), 'admin', NOW(), 'Заказ 2', 'admin', NOW(), 
     (SELECT id FROM users WHERE login = 'Cour1'), 
    (SELECT id FROM order_info2), (SELECT id FROM not_auth_users WHERE first_name = 'Олег'), NULL, 
     (SELECT id FROM users WHERE login = 'Basic'), '3a174d9d-c0e1-358f-01db-927e1290e9f1'),
(gen_random_uuid(), 'admin', NOW(), 'Заказ 3', 'admin', NOW(), 
     (SELECT id FROM users WHERE login = 'Cour1'), 
    (SELECT id FROM order_info3), NULL, (SELECT id FROM users WHERE login = 'Basic1'), 
     (SELECT id FROM users WHERE login = 'Basic'), '3a174d9d-c0e1-358f-01db-927e1290e9f1'),
(gen_random_uuid(), 'admin', NOW(), 'Заказ 4', 'admin', NOW(), 
     (SELECT id FROM users WHERE login = 'Cour'), 
    (SELECT id FROM order_info4), (SELECT id FROM not_auth_users WHERE first_name = 'Фарух'), NULL, 
     (SELECT id FROM users WHERE login = 'Basic1'), '3a174d9d-c0e1-358f-01db-927e1290e9f1'),
(gen_random_uuid(), 'admin', NOW(), 'Заказ 5', 'admin', NOW(), 
     (SELECT id FROM users WHERE login = 'Cour'), 
     (SELECT id FROM order_info5), NULL, (SELECT id FROM users WHERE login = 'Basic2'), 
     (SELECT id FROM users WHERE login = 'Basic1'), '3a174d9d-c0e1-358f-01db-927e1290e9f1'),
(gen_random_uuid(), 'admin', NOW(), 'Заказ 6', 'admin', NOW(), 
     null, (SELECT id FROM order_info6), (SELECT id FROM not_auth_users WHERE first_name = 'Алексей'), NULL, 
     (SELECT id FROM users WHERE login = 'Basic2'), '3a174d9d-c0e0-b9f8-286f-aa381bbf2d0a'),
(gen_random_uuid(), 'admin', NOW(), 'Заказ 7', 'admin', NOW(), 
     (SELECT id FROM users WHERE login = 'Cour'),
    (SELECT id FROM order_info7), NULL, (SELECT id FROM users WHERE login = 'Basic2'), 
     (SELECT id FROM users WHERE login = 'Basic'), '3a174d9d-c0e1-358f-01db-927e1290e9f1'),
(gen_random_uuid(), 'admin', NOW(), 'Заказ 8', 'admin', NOW(), 
     null, (SELECT id FROM order_info8), (SELECT id FROM not_auth_users WHERE first_name = 'Фарух'), NULL, 
     (SELECT id FROM users WHERE login = 'Basic1'), '3a174d9d-c0e0-b9f8-286f-aa381bbf2d0a');


#Создание Админа

INSERT INTO users (id, birth_day, created_date, first_name, last_name, login, password, role_id)  
VALUES (gen_random_uuid(), '1990-01-01', NOW(), 'Admin', 'Admin', 'Admin', '$2a$11$2OUblbk4LfMfGreEbndQIePViRI8JraT01MUiXAorN4yeCyUkq9z6', '3a1844b2-1e63-f523-2f04-f4eb3bad17b3');
