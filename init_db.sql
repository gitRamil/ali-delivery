#Пользователи вместе с паспорт инфо

WITH
passport1 AS (
    INSERT INTO public.passport_info (
        id, passport_info_expiration_date, passport_info_passport_number,
        passport_info_reg_date, passport_type_id, created_by,
        created_date, updated_by, updated_date
    )
    VALUES (
        gen_random_uuid(), '2030-04-12', '12314245241', '2020-04-12',
        '3a15d9e1-c9a0-80ab-eac9-9369b2ace783', 'admin',
        '2023-01-01 03:00:00+03'::timestamp with time zone, 'admin',
        '2023-01-01 03:00:00+03'::timestamp with time zone
    )
    RETURNING id
),
passport2 AS (
    INSERT INTO public.passport_info (
        id, passport_info_expiration_date, passport_info_passport_number,
        passport_info_reg_date, passport_type_id, created_by,
        created_date, updated_by, updated_date
    )
    VALUES (
        gen_random_uuid(), '2031-05-15', '56789012345', '2021-05-15',
        '3a15d9e1-c9a1-8b19-64f4-8cb3007b8a5d', 'admin',
        '2023-01-01 03:00:00+03'::timestamp with time zone, 'admin',
        '2023-01-01 03:00:00+03'::timestamp with time zone
    )
    RETURNING id
),
passport3 AS (
    INSERT INTO public.passport_info (
        id, passport_info_expiration_date, passport_info_passport_number,
        passport_info_reg_date, passport_type_id, created_by,
        created_date, updated_by, updated_date
    )
    VALUES (
        gen_random_uuid(), '2032-06-20', '98765432101', '2022-06-20',
        '3a15d9e1-c99f-95c5-162b-34f69121c4a1', 'admin',
        '2023-01-01 03:00:00+03'::timestamp with time zone, 'admin',
        '2023-01-01 03:00:00+03'::timestamp with time zone
    )
    RETURNING id
)
INSERT INTO public.users (
    id, passport_info_id, role_id, user_birth_day,
    user_first_name, user_last_name, created_by,
    created_date, updated_by, updated_date
)
VALUES
    (gen_random_uuid(), (SELECT id FROM passport1), '3a1537be-fa32-3962-f94d-62f95e6ffcad', '1990-01-01', 'Петр', 'Высший', 'admin', '2023-01-01 03:00:00+03'::timestamp with time zone, 'admin', '2023-01-01 03:00:00+03'::timestamp with time zone),
    (gen_random_uuid(), (SELECT id FROM passport2), '3a1537bf-cabc-d70c-f42c-012821b898b1', '1985-05-05', 'Вадим', 'Терещенко', 'admin', '2023-01-01 03:00:00+03'::timestamp with time zone, 'admin', '2023-01-01 03:00:00+03'::timestamp with time zone),
    (gen_random_uuid(), (SELECT id FROM passport3), '3a1537c0-11f8-d788-90d9-ced196c63397', '2000-12-12', 'Александр', 'Герасимов', 'admin', '2023-01-01 03:00:00+03'::timestamp with time zone, 'admin', '2023-01-01 03:00:00+03'::timestamp with time zone);


#Заказы вместе с пользователем, ордер инфо 

WITH 
order_info1 AS (
    INSERT INTO public.order_info (
        id, order_info_address_from, order_info_address_to,
        order_info_price, order_info_weight, size_id,
        created_by, created_date, updated_by, updated_date
    )
    VALUES (
        gen_random_uuid(), 'Address A1', 'Address B1', 150.00, 12.0,
        '3a156e1f-6056-875d-e42d-3e8e7ec6e082', 'admin', '2023-01-01 03:00:00+03'::timestamp with time zone,
        'admin', '2023-01-01 03:00:00+03'::timestamp with time zone
    )
    RETURNING id
),
order_info2 AS (
    INSERT INTO public.order_info (
        id, order_info_address_from, order_info_address_to,
        order_info_price, order_info_weight, size_id,
        created_by, created_date, updated_by, updated_date
    )
    VALUES (
        gen_random_uuid(), 'Address A2', 'Address B2', 200.00, 15.0,
        '3a156e1f-6056-875d-e42d-3e8e7ec6e082', 'admin', '2023-01-01 03:00:00+03'::timestamp with time zone,
        'admin', '2023-01-01 03:00:00+03'::timestamp with time zone
    )
    RETURNING id
),
order_info3 AS (
    INSERT INTO public.order_info (
        id, order_info_address_from, order_info_address_to,
        order_info_price, order_info_weight, size_id,
        created_by, created_date, updated_by, updated_date
    )
    VALUES (
        gen_random_uuid(), 'Address A3', 'Address B3', 250.00, 18.0,
        '3a156e1f-6056-875d-e42d-3e8e7ec6e082', 'admin', '2023-01-01 03:00:00+03'::timestamp with time zone,
        'admin', '2023-01-01 03:00:00+03'::timestamp with time zone
    )
    RETURNING id
)
INSERT INTO public.orders (
    id, name, order_info_id, order_status_id, sender_id,
    created_by, created_date, updated_by, updated_date
)
VALUES
    (gen_random_uuid(), 'Order 1', (SELECT id FROM order_info1), '3a15d9e1-c989-2e49-e8d3-55a56db7a2e1', '06af16fc-76c8-4344-b490-fd4844db0927', 'admin', '2023-01-01 03:00:00+03'::timestamp with time zone, 'admin', '2023-01-01 03:00:00+03'::timestamp with time zone),
    (gen_random_uuid(), 'Order 2', (SELECT id FROM order_info2), '3a15d9e1-c989-2e49-e8d3-55a56db7a2e1', 'bb3e6799-928a-4c89-92d7-4b6bea9e2121', 'admin', '2023-01-01 03:00:00+03'::timestamp with time zone, 'admin', '2023-01-01 03:00:00+03'::timestamp with time zone),
    (gen_random_uuid(), 'Order 3', (SELECT id FROM order_info3), '3a15d9e1-c99e-6357-1416-7c7be54dd2a5', 'b2050a3b-d0db-42f8-aa10-baaeba2d2a98', 'admin', '2023-01-01 03:00:00+03'::timestamp with time zone, 'admin', '2023-01-01 03:00:00+03'::timestamp with time zone);
