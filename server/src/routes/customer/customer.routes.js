const ApplicationRouter = require('express').Router();

const { authMiddleWare } = require('../../middleware/auth.middleware.js');

const {
  customerProfileControllerApi,
} = require('../../controllers/customer/customer.controller.api');

ApplicationRouter.route('/api/v1/Profile/me').get(
  authMiddleWare,
  customerProfileControllerApi
);

module.exports = ApplicationRouter;
