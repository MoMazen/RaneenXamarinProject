const {
  clientAddOrderControllerApi,
  clientGetOrderControllerApi,
} = require('../../controllers/order/order.controller.api.js');
const { authMiddleWare } = require('../../middleware/auth.middleware.js');

const applicationRoutes = require('express').Router();

applicationRoutes
  .route('/api/v1/order/add')
  .post(authMiddleWare, clientAddOrderControllerApi);
applicationRoutes
  .route('/api/v1/order')
  .get(authMiddleWare, clientGetOrderControllerApi);

module.exports = applicationRoutes;
