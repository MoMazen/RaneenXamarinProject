//we need to check before any request who is trying to access the end point

const { authService } = require('../services/auth/auth.service.js');

/**
 *
 * @param {Request} req
 * @param {Response} res
 * @param {any} next
 */
module.exports.authMiddleWare = async function (req, res, next) {
  //the role should stop the user from going next if he can not  tell who he is
  try {
    let user = await authService(req);

    //if all are ok

    req.user = user;

    next();
  } catch (error) {
    res.status(403).json({
      success: false,
      error: error,
    });
  }
};
