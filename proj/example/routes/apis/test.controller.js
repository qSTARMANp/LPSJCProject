const pool = require("../../config/database");

function id_check(req, res){
  pool.getConnection((err, connection) => {
    var sql = 'INSERT INTO User VALUES (id, pw, name, mail, y, m, d) (?,?,?,?,?,?,?);'
    var sqldata = [req.body.input_id,
      req.body.input_pw,
      req.body.input_name,
      req.body.input_mail,
      req.body.input_yy,
      req.body.input_mm,
      req.body.input_dd];
    connection.query(sql, sqldata, (err, results) => {
      if (!err){
        res.send(JSON.stringify(results));
        connection.release();
      }
      else {
          res.json({
            err: err,
            result: results
          })
      }
    })
  })
}

module.exports = { 
  id_check,
}