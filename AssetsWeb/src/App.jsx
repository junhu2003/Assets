import { useState, useEffect } from 'react'
import './App.css'

import { AssetsApi } from './assetsApi'

import Accordion from 'react-bootstrap/Accordion';
import AccordionBody from 'react-bootstrap/esm/AccordionBody';

import { FaTrash } from 'react-icons/fa';
import Row from 'react-bootstrap/Row';
import Col from 'react-bootstrap/Col';
import Form from 'react-bootstrap/Form';
import Button from 'react-bootstrap/Button';
import InputGroup from 'react-bootstrap/InputGroup';
import TableContainer from 'react-bootstrap/TabContainer';
import Modal from 'react-bootstrap/Modal';


function App() {
    const [inputs, setInputs] = useState({ name: '', value: 3000, categoryId: 0 })
    const [refresh, setRefresh] = useState(false)
    const [validated, setValidated] = useState(false)
    const [assets, setAssets] = useState([])
    const [categories, setCategories] = useState([])
    const [show, setShow] = useState(false);
    const [assetId, setAssetId] = useState(0);

    const handleClose = () => {
        setAssetId(0);
        setShow(false);
    }

    const handleShow = (event, id) => {
        event.preventDefault();

        if (id) {
            setAssetId(id);
            setShow(true);
        }
    }

    useEffect(() => {
        const debounce = setTimeout(() => {
            populateAssets();
            populateCategories();

        }, 1000)
        return () => clearTimeout(debounce);
    }, [refresh]);

    const handleChange = (event) => {
        const name = event.target.name;
        const value = event.target.value;
        setInputs(values => ({ ...values, [name]: value }));
    }

    const addItem = () => {
        if (inputs.name === '' || inputs.value === 0 || inputs.categoryId === 0) {
            setValidated(false);
            return;
        }

        var asset = { name: inputs.name, value: inputs.value, categoryId: inputs.categoryId };
        AssetsApi.addAsset(asset)
            .then(() => {
                setRefresh(!refresh);
            })
            .catch(() => {
                alert('Fail to add new asset!');
            });
    }

    const deleteItem = () => {

        if (assetId > 0) {
            AssetsApi.deleteAssetById(assetId)
                .then(() => {
                    setRefresh(!refresh);
                })
                .catch(() => {
                    alert('Fail to delete asset!');
                });
        }        

        handleClose();
    }

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
                                                                <a href="/" onClick={(e) => handleShow(e, asset.id)}>
                                                                    <object>
                                                                        <FaTrash onClick={deleteItem} />
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

            <Form noValidate validated={validated}>
                <Row>
                    <Col lg="16">
                        <InputGroup>
                            <Form.Group as={Col} lg="3" controlId="validationName" className='formGroup'>
                                <Form.Control
                                    type="text"
                                    name="name"
                                    placeholder="Item Name"
                                    value={inputs.name}
                                    onChange={handleChange}
                                    required                                    
                                />                                
                            </Form.Group>

                            <Form.Group as={Col} lg="3" controlId="validationValue" className='formGroup'>
                                <Form.Control
                                    type="number"
                                    name="value"
                                    placeholder="Value"
                                    value={inputs.value}
                                    onChange={handleChange}
                                    required                                    
                                />                                
                            </Form.Group>                            

                            <Form.Group as={Col} lg="4" controlId="validationCategory" className='formGroup'>
                                <Form.Select
                                    as="select"
                                    name="categoryId"
                                    placeholder="Category"
                                    value={inputs.categoryId}
                                    onChange={handleChange}
                                    required                                    
                                >
                                    <option value="0" key="0">Select Category</option>
                                    {categories && categories.map((c, i) => <option key={i + 1} value={c.id}>{c.name}</option>)}
                                </Form.Select>
                            </Form.Group>

                            <Button variant="primary" type="button" onClick={addItem} className='formGroup'>
                                Add Item
                            </Button>
                            
                        </InputGroup>
                    </Col>
                </Row>
            </Form>

            <Modal show={show} onHide={handleClose}>
                <Modal.Header closeButton>
                    <Modal.Title>Confirm to delete asset</Modal.Title>
                </Modal.Header>
                <Modal.Body>Are you sure to delete this asset?</Modal.Body>
                <Modal.Footer>
                    <Button variant="secondary" onClick={handleClose}>
                        Close
                    </Button>
                    <Button variant="primary" onClick={deleteItem}>
                        Delete
                    </Button>
                </Modal.Footer>
            </Modal>
    </div>
  )
}

export default App
