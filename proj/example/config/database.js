const mysql = require("mysql");
const pool= mysql.createPool({
    connectionLimit: 10,
    host: "localhost",
    user: "root",
    password: "root",
    database: "proj",
    port: 3308,
});

module.exports = pool;