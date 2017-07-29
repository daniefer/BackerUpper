import { BackerUpper } from './app.po';

describe('backer-upper.web App', () => {
  let page: BackerUpper;

  beforeEach(() => {
    page = new BackerUpper();
  });

  it('should display welcome message', () => {
    page.navigateTo();
    expect(page.getParagraphText()).toEqual('Welcome to app!');
  });
});
