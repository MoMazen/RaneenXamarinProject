const path = require('path');

require('dotenv').config({
  path: path.join(__dirname, './config', 'config.env'),
});

const serverApplication = require('./src/app/server.js');

const app = new serverApplication().init();
