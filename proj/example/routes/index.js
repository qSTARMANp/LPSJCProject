var express = require('express');
var router = express.Router();

const apisRouter = require('./apis/test.routes');

/* GET home page. */
router.get('/', function(req, res, next) {
  res.render('index', { title: 'Express' });
});

router.get('/test', function(req, res, next) {
  res.render('test.html');
});

router.get('/new', function(req, res, next) {
  res.render('new.html');
});

router.get('/main', function(req, res, next) {
  res.render('main.html');
});

router.get('/update', function(req, res, next) {
  res.render('update.html');
});

router.get('/login', function(req, res, next) {
  res.render('login.html');
});

router.use("/api", apisRouter);

module.exports = router;
