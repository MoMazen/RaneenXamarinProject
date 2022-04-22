const applicationRoutes = require('express').Router();

const { authMiddleWare } = require('../../middleware/auth.middleware.js');

const {
  clientAddressAttacherControllerApi,
} = require('../../controllers/address/address.controller.api.js');

applicationRoutes
  .route('/api/v1/client/address/attach')
  .post(authMiddleWare, clientAddressAttacherControllerApi);

module.exports = applicationRoutes;
