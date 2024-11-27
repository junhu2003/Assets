import { useState, useEffect } from 'react'
import './App.css'

import { AssetsApi } from './assetsApi'

import Accordion from 'react-bootstrap/Accordion';
import AccordionBody from 'react-bootstrap/esm/AccordionBody';
import TableContainer from 'react-bootstrap/TabContainer';
import { FaTrash } from 'react-icons/fa';

function App() {    
    const [assets, setAssets] = useState([])
    const [categories, setCategories] = useState([])

    useEffect(() => {
        const debounce = setTimeout(() => {
            populateAssets();
            populateCategories();

        }, 1000)
        return () => clearTimeout(debounce);
    }, []);

    const populateAssets = () => {

        AssetsApi.getAssets()
            .then((json) => {
                json ? setAssets(json) : []                
            })
            .catch(() => {
                setAssets([]);                
            });
    }

    const populateCategories = () => {

        AssetsApi.getCategories()
            .then((json) => {
                json ? setCategories(json) : []
            })
            .catch(() => {
                setCategories([]);
            });
    }

    return (
        <div>
            {
              categories && categories.length > 0 && assets && assets.length > 0 &&
                  categories.map((category, i) => {                  
                      var assetsByCategory = assets.filter(asset => asset.categoryId == category.id);
                      var sum = assetsByCategory.map(a => a.value).reduce(function (a, b) {
                          return a + b;
                      });

                      return <Accordion key={'Accordion' + i} defaultActiveKey="2" alwaysOpen={true} hidden={!assetsByCategory || assetsByCategory.length === 0}>
                                <Accordion.Item eventKey="Category">
                                    <Accordion.Header>
                                        <div className="container">
                                            <div className="row">
                                                <div className="col-md-6">{category.name}</div>
                                                <div className="col-md-6"><span className="pull-right">${sum}</span></div>
                                            </div>
                                        </div>
                                    </Accordion.Header>
                                    <AccordionBody>
                                        <TableContainer>
                                            {
                                          assetsByCategory.map((asset, j) => {
                                              return <div key={j} className="container ">
                                                        <div className="row">
                                                            <div className="col-md-8">{asset.name}</div>
                                                            <div className="col-md-2">${asset.value}</div>
                                                            <div className="col-md-2">
                                                                <a href="#">
                                                                    <object>
                                                                        <FaTrash />
                                                                    </object>
                                                                </a>
                                                            </div>
                                                        </div>
                                                      </div>;
                                                })
                                            }
                                        </TableContainer>
                                    </AccordionBody>
                                </Accordion.Item>
                             </Accordion>;                      
                  })

          }
    </div>
  )
}

export default App
