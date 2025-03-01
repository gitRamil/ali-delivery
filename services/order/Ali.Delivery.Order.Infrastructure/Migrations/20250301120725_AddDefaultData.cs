using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ali.Delivery.Order.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddDefaultData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //Создание Users
            migrationBuilder.Sql(@"
                WITH
                passport1 AS (
                    INSERT INTO passport (id, passport_number, registration_date, type_id, created_by, created_date, updated_by, updated_date, issued_by)
                    VALUES ('91b734a8-09a0-43e9-ad9e-53f0a9742413'::uuid, '77777777777', '2020-04-12', '3a15d9e1-c9a0-80ab-eac9-9369b2ace783'::uuid, 'admin',  NOW(), 'admin', NOW(), 'MVD RF')
                    RETURNING id
                ),
                passport2 AS (
                    INSERT INTO passport (id, passport_number, registration_date, type_id, created_by, created_date, updated_by, updated_date, issued_by)
                    VALUES ('1b15d9e1-c9a1-84ab-eac9-9369b2ace583'::uuid, '999999999', '2021-04-12', '3a15d9e1-c9a1-8b19-64f4-8cb3007b8a5d'::uuid, 'admin', NOW(), 'admin', NOW(), 'MVD RK')
                    RETURNING id
                ),
                passport3 AS (
                    INSERT INTO passport (id, passport_number, registration_date, type_id, created_by, created_date, updated_by, updated_date, issued_by)
                    VALUES ('666a5776-996e-47db-a48f-98bcd2f1f36a'::uuid, '8888888888', '2018-04-12', '3a15d9e1-c9a0-80ab-eac9-9369b2ace783'::uuid, 'admin', NOW(), 'admin', NOW(), 'MVD RF')
                    RETURNING id
                ),
                passport4 AS (
                    INSERT INTO passport (id, passport_number, registration_date, type_id, created_by, created_date, updated_by, updated_date, issued_by)
                    VALUES ('b9d94ace-6195-4a82-a228-8ed08e9a079a'::uuid, '444444444', '2015-04-12', '3a15d9e1-c9a0-80ab-eac9-9369b2ace783'::uuid, 'admin', NOW(), 'admin', NOW(), 'MVD RF')
                    RETURNING id
                ),
                passport5 AS (
                    INSERT INTO passport (id, passport_number, registration_date, type_id, created_by, created_date, updated_by, updated_date, issued_by)
                    VALUES ('9eeadb8a-e8bd-4ea8-8b96-20c67bade35d'::uuid, '8888888888', '2016-04-12', '3a15d9e1-c9a1-8b19-64f4-8cb3007b8a5d'::uuid, 'admin', NOW(), 'admin', NOW(), 'MVD RK')
                    RETURNING id
                ),
                passport6 AS (
                    INSERT INTO passport (id, passport_number, registration_date, type_id, created_by, created_date, updated_by, updated_date, issued_by)
                    VALUES ('9019eeb1-e73a-4163-b7d7-cfb0ebc4e2e5'::uuid, '8888888888', '2012-04-12', '3a15d9e1-c9a1-8b19-64f4-8cb3007b8a5d'::uuid, 'admin', NOW(), 'admin', NOW(),'MVD RK')
                    RETURNING id
                )
                INSERT INTO users (id, login, passport_info_id, password, role_id, birth_day, first_name, last_name, created_by, created_date, updated_by, updated_date)
                VALUES 
                    ('27bd6e66-6a34-4205-8f99-2e3eb9bd4e35'::uuid, 'Basic', (SELECT id FROM passport1), '$2a$11$Ks7AEOVVMDqsIjzpFEaHb.c9NfzeqBk/QhPnuiqbERfE186QBjS4a','3a1844b2-1e4f-f70c-c599-5fe824c7a873', '1990-01-01', 'Петр', 'Высший', 'Admin', NOW(), 'Admin' ,NOW()),
                    ('c2c64630-a694-4576-82fb-223675783c13'::uuid, 'Basic1', (SELECT id FROM passport4), '$2a$11$Ks7AEOVVMDqsIjzpFEaHb.c9NfzeqBk/QhPnuiqbERfE186QBjS4a','3a1844b2-1e4f-f70c-c599-5fe824c7a873', '1985-05-05', 'Вадим', 'Терещенко', 'Admin', NOW(), 'Admin' ,NOW()),
                    ('d1f45a30-8d33-4c1a-9f2d-bc64b6a732d9'::uuid, 'Basic2', (SELECT id FROM passport3), '$2a$11$Ks7AEOVVMDqsIjzpFEaHb.c9NfzeqBk/QhPnuiqbERfE186QBjS4a','3a1844b2-1e4f-f70c-c599-5fe824c7a873', '2000-12-12', 'Александр', 'Герасимов', 'Admin', NOW(), 'Admin' ,NOW()),
                    ('bfa9a5de-f40e-4f6c-8c47-2399f64bcbfb'::uuid, 'Cour', (SELECT id FROM passport2), '$2a$11$Ks7AEOVVMDqsIjzpFEaHb.c9NfzeqBk/QhPnuiqbERfE186QBjS4a','3a1844b2-1e60-6e25-1bcf-da74b2a4f07c', '1990-01-01', 'Иван', 'Логов', 'Admin', NOW(), 'Admin' ,NOW()),
                    ('6b1724c8-5e2b-4195-87a1-3dd9372cce93'::uuid, 'Cour1', (SELECT id FROM passport5), '$2a$11$Ks7AEOVVMDqsIjzpFEaHb.c9NfzeqBk/QhPnuiqbERfE186QBjS4a','3a1844b2-1e60-6e25-1bcf-da74b2a4f07c', '1985-05-05', 'Михаил', 'Осетин', 'Admin', NOW(), 'Admin' ,NOW()),
                    ('f6293f04-b429-4e95-94f7-97f793c00904'::uuid, 'Cour2', (SELECT id FROM passport6), '$2a$11$Ks7AEOVVMDqsIjzpFEaHb.c9NfzeqBk/QhPnuiqbERfE186QBjS4a','3a1844b2-1e60-6e25-1bcf-da74b2a4f07c', '2000-12-12', 'Даниил', 'Герц', 'Admin', NOW(), 'Admin' ,NOW());");

            // Создание NotAuthUsers.
            migrationBuilder.Sql(@"
                INSERT INTO not_auth_users (id, first_name, last_name, phone_number, creator_user_id)
                SELECT '13f3c230-17b4-45a3-97d8-e89b932a52e3'::uuid, 'Олег', 'Петров', '+7778885566', id FROM users WHERE login = 'Basic'
                UNION ALL
                SELECT '2a60ff20-07a1-4f95-8992-571cbbf4b9a2'::uuid, 'Фарух', 'Муродов', '+7777775566', id FROM users WHERE login = 'Basic1'
                UNION ALL
                SELECT 'e12b3e4e-f0c7-4991-b2b8-3049c4c70bde'::uuid, 'Алексей', 'Иванов', '+7779995566', id FROM users WHERE login = 'Basic2';
                ");

            // Создание заказов.
            migrationBuilder.Sql(@"
                WITH 
                order_info1 AS (
                    INSERT INTO order_details (id ,address_from, address_to, price, weight, size_id, created_by, created_date, updated_by, updated_date)
                    VALUES ('7f935f9a-0e4b-44f6-bc95-4d19d9a9f12a'::uuid, 'Address A1', 'Address B1', 150.00, 200.0, '3a156e1f-6055-abfb-36b7-7e630cc807b9'::uuid, 'admin', NOW(), 'admin', NOW())
                    RETURNING id
                ),
                order_info2 AS (
                    INSERT INTO order_details (id ,address_from, address_to, price, weight, size_id, created_by, created_date, updated_by, updated_date)
                    VALUES ('b4b4bb92-66cb-4bfb-91df-70b8aa450f79'::uuid, 'Address A2', 'Address B2', 1500, 500,'3a156e1f-6055-abfb-36b7-7e630cc807b9'::uuid, 'admin', NOW(), 'admin', NOW())
                    RETURNING id
                ),
                order_info3 AS (
                    INSERT INTO order_details (id ,address_from, address_to, price, weight, size_id, created_by, created_date, updated_by, updated_date)
                    VALUES ('32b1cc5e-285b-4d24-8734-19d6c2752a42'::uuid,'Address A3', 'Address B3', 1550, 510,'3a156e1f-6055-abfb-36b7-7e630cc807b9'::uuid, 'admin', NOW(), 'admin', NOW())
                    RETURNING id
                ),
                order_info4 AS (
                    INSERT INTO order_details (id ,address_from, address_to, price, weight, size_id, created_by, created_date, updated_by, updated_date)
                    VALUES ('9910ad36-5c6b-4ee6-9c79-b5dc4995df6c'::uuid, 'Address A4', 'Address B4', 1500.00, 450.0, '3a156e1f-6056-875d-e42d-3e8e7ec6e082'::uuid, 'admin', NOW(), 'admin', NOW())
                    RETURNING id
                ),
                order_info5 AS (
                    INSERT INTO order_details (id ,address_from, address_to, price, weight, size_id, created_by, created_date, updated_by, updated_date)
                    VALUES ('8c3ebcb5-19a3-417a-a604-64f4529d6ae4'::uuid, 'Address A5', 'Address B5', 10000.00, 1000.0, '3a156e1f-6057-39cd-7580-20395231a00f'::uuid, 'admin', NOW(), 'admin', NOW())
                    RETURNING id
                ),
                order_info6 AS (
                    INSERT INTO order_details (id ,address_from, address_to, price, weight, size_id, created_by, created_date, updated_by, updated_date)
                    VALUES ('8502d2a4-5d9c-436f-b988-f32fdfd7b343'::uuid, 'Address A6', 'Address B6', 15000.00, 800.0, '3a156e1f-6057-39cd-7580-20395231a00f'::uuid, 'admin', NOW(), 'admin', NOW())
                    RETURNING id
                ),
                order_info7 AS (
                    INSERT INTO order_details (id ,address_from, address_to, price, weight, size_id, created_by, created_date, updated_by, updated_date)
                    VALUES ('1882b97c-715f-45a5-894d-cfb2fdaf06dc'::uuid, 'Address A7', 'Address B7', 1700.00, 650.0, '3a156e1f-6056-875d-e42d-3e8e7ec6e082'::uuid, 'admin', NOW(), 'admin', NOW())
                    RETURNING id
                ),
                order_info8 AS (
                    INSERT INTO order_details (id ,address_from, address_to, price, weight, size_id, created_by, created_date, updated_by, updated_date)
                    VALUES ('d1c932ca-40dc-431c-95c2-00c1409cf983'::uuid, 'Address A8', 'Address B8', 1550.00, 550.0, '3a156e1f-6056-875d-e42d-3e8e7ec6e082'::uuid, 'admin', NOW(), 'admin', NOW())
                    RETURNING id
                )
                INSERT INTO orders (id, created_by, created_date, name, updated_by, updated_date, courier_id, details_id, not_auth_receiver_id, receiver_id, sender_id, status_id)
                VALUES
                    ('78986e08-b474-48aa-867e-c01da770d58c'::uuid, 'admin', NOW(), 'Заказ 1', 'admin', NOW(), null, (SELECT id FROM order_info1), NULL, (SELECT id FROM users WHERE login = 'Basic'), (SELECT id FROM users WHERE login = 'Basic1'), '3a174d9d-c0e0-b9f8-286f-aa381bbf2d0a'::uuid),
                    ('8c479c8f-7047-41d3-aafa-86925339b3bd'::uuid, 'admin', NOW(), 'Заказ 2', 'admin', NOW(), (SELECT id FROM users WHERE login = 'Cour1'), (SELECT id FROM order_info2), (SELECT id FROM not_auth_users WHERE first_name = 'Олег'), NULL, (SELECT id FROM users WHERE login = 'Basic'), '3a174d9d-c0e1-358f-01db-927e1290e9f1'::uuid),
                    ('8545215b-580b-4080-b4c1-9d2707e08eae'::uuid, 'admin', NOW(), 'Заказ 3', 'admin', NOW(), (SELECT id FROM users WHERE login = 'Cour1'),    (SELECT id FROM order_info3), NULL, (SELECT id FROM users WHERE login = 'Basic1'), (SELECT id FROM users WHERE login = 'Basic'), '3a174d9d-c0e1-358f-01db-927e1290e9f1'::uuid),
                    ('14a3c9f9-bfbe-4e22-a707-e1339ff1bcb4'::uuid, 'admin', NOW(), 'Заказ 4', 'admin', NOW(), (SELECT id FROM users WHERE login = 'Cour'), (SELECT id FROM order_info4), (SELECT id FROM not_auth_users WHERE first_name = 'Фарух'), NULL, (SELECT id FROM users WHERE login = 'Basic1'), '3a174d9d-c0e1-358f-01db-927e1290e9f1'),
                    ('a1622758-fdb7-4e8e-993d-3984474f013f'::uuid, 'admin', NOW(), 'Заказ 5', 'admin', NOW(), (SELECT id FROM users WHERE login = 'Cour'),(SELECT id FROM order_info5), NULL, (SELECT id FROM users WHERE login = 'Basic2'), (SELECT id FROM users WHERE login = 'Basic1'), '3a174d9d-c0e1-358f-01db-927e1290e9f1'::uuid),
                    ('5a465a38-bfd6-4cef-b453-268e25cfbf69'::uuid, 'admin', NOW(), 'Заказ 6', 'admin', NOW(), null, (SELECT id FROM order_info6), (SELECT id FROM not_auth_users WHERE first_name = 'Алексей'), NULL, (SELECT id FROM users WHERE login = 'Basic2'), '3a174d9d-c0e0-b9f8-286f-aa381bbf2d0a'),
                    ('61635b24-cca7-4e1d-a14f-085f75c79edf'::uuid, 'admin', NOW(), 'Заказ 7', 'admin', NOW(), (SELECT id FROM users WHERE login = 'Cour'), (SELECT id FROM order_info7), NULL, (SELECT id FROM users WHERE login = 'Basic2'), (SELECT id FROM users WHERE login = 'Basic'), '3a174d9d-c0e1-358f-01db-927e1290e9f1'::uuid),
                    ('86ec66db-7e5a-490d-933b-4e57bd98a5dd'::uuid, 'admin', NOW(), 'Заказ 8', 'admin', NOW(), null, (SELECT id FROM order_info8), (SELECT id FROM not_auth_users WHERE first_name = 'Фарух'), NULL, (SELECT id FROM users WHERE login = 'Basic1'), '3a174d9d-c0e0-b9f8-286f-aa381bbf2d0a'::uuid);
                ");
            // Создание SuperUser.
            migrationBuilder.Sql(@"
                INSERT INTO users (id, birth_day, created_date, first_name, last_name, login, password, role_id)  
                VALUES ('cfcbcc3b-994b-4c36-a28a-2a1a19aa01fd'::uuid, '1990-01-01', NOW(), 'Admin', 'Admin', 'Admin', '$2a$11$2OUblbk4LfMfGreEbndQIePViRI8JraT01MUiXAorN4yeCyUkq9z6', '3a1844b2-1e63-f523-2f04-f4eb3bad17b3'::uuid);");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                DELETE FROM orders 
                WHERE id IN (
                    '78986e08-b474-48aa-867e-c01da770d58c',
                    '8c479c8f-7047-41d3-aafa-86925339b3bd',
                    '8545215b-580b-4080-b4c1-9d2707e08eae',
                    '14a3c9f9-bfbe-4e22-a707-e1339ff1bcb4',
                    'a1622758-fdb7-4e8e-993d-3984474f013f',
                    '5a465a38-bfd6-4cef-b453-268e25cfbf69',
                    '61635b24-cca7-4e1d-a14f-085f75c79edf',
                    '86ec66db-7e5a-490d-933b-4e57bd98a5dd');

                DELETE FROM order_details 
                WHERE id IN (
                    '7f935f9a-0e4b-44f6-bc95-4d19d9a9f12a',
                    'b4b4bb92-66cb-4bfb-91df-70b8aa450f79',
                    '32b1cc5e-285b-4d24-8734-19d6c2752a42',
                    '9910ad36-5c6b-4ee6-9c79-b5dc4995df6c',
                    '8c3ebcb5-19a3-417a-a604-64f4529d6ae4',
                    '8502d2a4-5d9c-436f-b988-f32fdfd7b343',
                    '1882b97c-715f-45a5-894d-cfb2fdaf06dc',
                    'd1c932ca-40dc-431c-95c2-00c1409cf983');
            ");
            
            migrationBuilder.Sql(@"
                DELETE FROM not_auth_users 
                WHERE id IN (
                    '13f3c230-17b4-45a3-97d8-e89b932a52e3',
                    '2a60ff20-07a1-4f95-8992-571cbbf4b9a2',
                    'e12b3e4e-f0c7-4991-b2b8-3049c4c70bde');
            ");
            
            // Удаление пользователей
            migrationBuilder.Sql(@"
            DELETE FROM users 
            WHERE id IN (
                '27bd6e66-6a34-4205-8f99-2e3eb9bd4e35',
                'c2c64630-a694-4576-82fb-223675783c13',
                'd1f45a30-8d33-4c1a-9f2d-bc64b6a732d9',
                'bfa9a5de-f40e-4f6c-8c47-2399f64bcbfb',
                '6b1724c8-5e2b-4195-87a1-3dd9372cce93',
                'f6293f04-b429-4e95-94f7-97f793c00904',
                'cfcbcc3b-994b-4c36-a28a-2a1a19aa01fd')
            ");
            
            // Удаление паспортов
            migrationBuilder.Sql(@"
            DELETE FROM passport 
            WHERE id IN (
                '91b734a8-09a0-43e9-ad9e-53f0a9742413',
                '1b15d9e1-c9a1-84ab-eac9-9369b2ace583',
                '666a5776-996e-47db-a48f-98bcd2f1f36a',
                'b9d94ace-6195-4a82-a228-8ed08e9a079a',
                '9eeadb8a-e8bd-4ea8-8b96-20c67bade35d',
                '9019eeb1-e73a-4163-b7d7-cfb0ebc4e2e5')
            ");

        }
    }
}
