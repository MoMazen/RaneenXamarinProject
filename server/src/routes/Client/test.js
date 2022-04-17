const applicationRouter = require('express').Router();

const { authMiddleWare } = require('../../middleware/auth.middleware.js');

applicationRouter
  .route('/api/v1/client/test')
  .get(authMiddleWare, (req, res) => {
    res.status(200).json({
      data: 'u are authorized and authenticated',
      user: req.user,
    });
  });

module.exports = applicationRouter;
