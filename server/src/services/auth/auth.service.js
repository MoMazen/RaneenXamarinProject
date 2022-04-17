const { request } = require('express');
const jsonwebtoken = require('jsonwebtoken');

/**
 *
 * @param {request} httpRequestObj
 */
module.exports.authService = async function (httpRequestObj) {
  return new Promise(async (resolve, reject) => {
    try {
      let token = httpRequestObj.headers['x-auth-token'];

      if (token) {
        try {
          //suppose that the user is admin
          let user = jsonwebtoken.verify(token, process.env.ADMIN_SECRET);
          //if it's ok no errors
          user.role = 'ADMIN';
          resolve(user);
        } catch (error) {
          try {
            //suppose that the user is admin
            let user = jsonwebtoken.verify(token, process.env.CLIENT_SECRET);
            //if it's ok no errors
            user.role = 'CLIENT';
            resolve(user);
          } catch (err) {
            reject({
              message: 'UNAUTHORIZED',
            });
          }
        }
      } else {
        reject({
          message: new Error('UNAUTHORIZED').message,
        });
      }
    } catch (error) {
      reject({
        message: error,
      });
    }
  });
};
