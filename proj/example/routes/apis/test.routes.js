const express = require('express');
const router = express.Router();

const controller = require('./test.controller');
router.post('/id_check', controller.id_check);

module.exports = router;