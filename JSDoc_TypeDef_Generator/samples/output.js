/**
 * Enter Description Here
 * @typedef {Object} MyType
 * @property {string} tabsRoot 
 * @property {string} packsRoot 
 * @property {string} emoticonsRoot 
 * @property {string} itemsRoot 
 * @property {tab[]} tabs 
 * @property {pack[]} packs 
 * @property {item[]} items 
 */
/**
 * Enter Description Here
 * @typedef {Object} item
 * @property {string} id 
 * @property {string} type 
 * @property {string[]} shortcuts 
 * @property {boolean} visible 
 * @property {boolean} useInSms 
 * @property {media} media 
 * @property {string} description 
 * @property {string[]} keywords 
 * @property {string} etag 
 */
/**
 * Enter Description Here
 * @typedef {Object} media
 * @property {default} default 
 */
/**
 * Enter Description Here
 * @typedef {Object} default
 * @property {number} firstFrame 
 * @property {number} framesCount 
 * @property {number} framesCountOptimized 
 * @property {number[]} playBack 
 * @property {number} fps 
 */
/**
 * Enter Description Here
 * @typedef {Object} pack
 * @property {string} id 
 * @property {string} title 
 * @property {string} description 
 * @property {any} copyright 
 * @property {boolean} isHidden 
 * @property {boolean} isSponsored 
 * @property {any} keywords 
 * @property {string} price 
 * @property {any} expiry 
 * @property {string} etag 
 * @property {string[]} items 
 */
/**
 * Enter Description Here
 * @typedef {Object} tab
 * @property {section[]} sections 
 * @property {string} id 
 * @property {string} title 
 * @property {string} description 
 * @property {string} copyright 
 * @property {boolean} isHidden 
 * @property {string} price 
 * @property {any} expiry 
 * @property {string} glyphBgColor 
 * @property {boolean} isDiscoverable 
 * @property {string} badgeETag 
 * @property {string[]} keywords 
 * @property {string} etag 
 */
/**
 * Enter Description Here
 * @typedef {Object} section
 * @property {string} pack 
 */
